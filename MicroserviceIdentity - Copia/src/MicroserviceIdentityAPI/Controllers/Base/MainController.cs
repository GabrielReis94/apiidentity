using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MicroserviceIdentityAPI.Domain.Domain.Core.Communication;
using MicroserviceIdentityAPI.Domain.Models;

namespace MicroserviceIdentityAPI.Controllers.Base
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if(ValidOperation())
            {
                if(result == null)
                    return Ok();
                    
                return Ok(result);
            }

            return BadRequest(new ValidationProblemsResponse(new Dictionary<string, string[]>
            {
                {"Messages", Errors.ToArray() }
            }));
        }        

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                AddErrorToStack(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomReponse(List<string> Errors)
        {
            foreach (var error in Errors)
            {
                AddErrorToStack(error);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(string message, bool success = false)
        {
            if(!success)
                AddErrorToStack(message);

            return CustomResponse();
        }
        
        protected ActionResult CustomResponse(ResponseResult responseResult)
        {
            ResponseHasErrors(responseResult);

            return CustomResponse();
        }

        protected ActionResult CustomResponseInternalError(string message)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, message);
        }

        protected ActionResult CustomResponseInternalError(GenericErrorViewModel genericErrorViewModel)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                genericErrorViewModel.Message);
        }

        protected bool ResponseHasErrors(ResponseResult responseResult)
        {
            if(responseResult == null || !responseResult.Errors.Messages.Any()) return false;

            foreach (var ErrorMessage in responseResult.Errors.Messages)
            {
                AddErrorToStack(ErrorMessage);
            }

            return true;
        }

        protected bool ValidOperation()
        {
            return !Errors.Any();
        }

        protected void AddErrorToStack(string error)
        {
            Errors.Add(error);
        }

        protected void CleanErrors()
        {
            Errors.Clear();
        }
    }
}