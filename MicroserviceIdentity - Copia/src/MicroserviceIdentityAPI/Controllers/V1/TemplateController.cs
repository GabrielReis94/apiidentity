using Microsoft.AspNetCore.Mvc;
using MicroserviceIdentityAPI.Application.Interfaces;
using MicroserviceIdentityAPI.Controllers.Base;
using MicroserviceIdentityAPI.Domain.DTO;
using MicroserviceIdentityAPI.Domain.Models;
using MicroserviceIdentityAPI.Filters;
using Swashbuckle.AspNetCore.Annotations;

namespace MicroserviceIdentityAPI.Controllers.V1
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class TemplateController : MainController
    {
        private readonly IApplicationServiceExample _applicationServiceExample;

        public TemplateController(IApplicationServiceExample applicationServiceExample)
        {
            _applicationServiceExample = applicationServiceExample;
        }

        [HttpPost]
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao registrar exemplo")]
        [SwaggerResponse(statusCode: 400, description: "Erro nos campos", type: typeof(ValidateInputViewModel))]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", type: typeof(GenericErrorViewModel))]
        [CustomModelStateValidation]
        [Route("add")]
        public async Task<IActionResult> Post(ExampleDTO example)
        {
            try
            {
                return CustomResponse(await _applicationServiceExample.AddAsync(example));
            }
            catch (Exception ex)
            {                
                return CustomResponseInternalError("Erro ao processar a requisição. Por favor contate a equipe de suporte! Mensagem: "
                         + ex.Message);
            }
        }
    }
}