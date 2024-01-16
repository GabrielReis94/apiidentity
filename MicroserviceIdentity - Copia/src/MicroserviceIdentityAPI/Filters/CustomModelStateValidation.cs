using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MicroserviceIdentityAPI.Domain.Models;

namespace MicroserviceIdentityAPI.Filters
{
    public class CustomModelStateValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                List<ErrorModel> lstErros = new List<ErrorModel>();

                var propertys = context.ModelState.Keys.ToArray();
                var values = context.ModelState.Values.ToArray();

                for (int i = 0; i < context.ModelState.Keys.Count(); i++)
                {
                    lstErros.Add(new ErrorModel
                    {
                        Property = propertys[i].ToString(),
                        Message = values[i].Errors.FirstOrDefault().ErrorMessage
                    });
                }

                var validateInputViewModel = new ValidateInputViewModel(lstErros);

                context.Result = new BadRequestObjectResult(validateInputViewModel);
            }
        }
    }
}