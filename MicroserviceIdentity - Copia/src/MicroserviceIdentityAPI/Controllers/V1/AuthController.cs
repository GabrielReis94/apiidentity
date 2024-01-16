using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroserviceIdentityAPI.Controllers.Base;
using MicroserviceIdentityAPI.Domain.Models;
using MicroserviceIdentityAPI.Filters;
using MicroserviceIdentityAPI.Shared.Models;
using MicroserviceIdentityAPI.Shared.Security;
using MicroserviceIdentityAPI.Shared.TokenServices;
using Swashbuckle.AspNetCore.Annotations;

namespace MicroserviceIdentityAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class AuthController : MainController
    {
        [HttpPost]
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao efetuar login")]
        [SwaggerResponse(statusCode: 400, description: "Erro nos campos", type: typeof(ValidateInputViewModel))]
        [SwaggerResponse(statusCode: 500, description: "Erro ao efeturar login", type: typeof(GenericErrorViewModel))]
        [CustomModelStateValidation]
        [AllowAnonymous]
        [Route("login")]
        public Token Post(AccessCredentials credentials,
                [FromServices] AccessManager accessManager,
                [FromServices] TokenService tokenService)
        {
            if(accessManager.ValidateCredentials(credentials))
                return tokenService.GenerateToken(credentials);
            else
            {
                return new Token
                {
                    Authenticated = false,
                    Message = "Falha ao realizar login"
                };
            }
        }
    }
}