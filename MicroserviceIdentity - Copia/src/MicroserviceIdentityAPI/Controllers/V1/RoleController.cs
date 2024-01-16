using MicroserviceIdentityAPI.Application.Interfaces;
using MicroserviceIdentityAPI.Controllers.Base;
using MicroserviceIdentityAPI.Domain.DTO;
using MicroserviceIdentityAPI.Domain.Models;
using MicroserviceIdentityAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace MicroserviceIdentityAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class RoleController : MainController
    {
        private readonly IApplicationServiceRoles _applicationServiceRoles;

        public RoleController(IApplicationServiceRoles applicationServiceRoles)
        {
            _applicationServiceRoles = applicationServiceRoles;
        }

        [HttpPost]
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao registrar a permissao")]
        [SwaggerResponse(statusCode: 400, description: "Erro nos campos", type: typeof(ValidateInputViewModel))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", type: typeof(GenericErrorViewModel))]
        [CustomModelStateValidation]
        [Route("add")]
        public async Task<IActionResult> Post(RoleDTO role)
        {
            try
            {   
                var result = await _applicationServiceRoles.AddAsync(role);
                if(!result)
                    AddErrorToStack("Erro ao cadastrar a permissão de acesso. Por favor contate a equipe de suporte!");
                
                return CustomResponse("Cadastrado com sucesso", result);
                    
            }
            catch (Exception)
            {                
                return CustomResponseInternalError("Erro ao cadastrar a permissão de acesso. Por favor contate a equipe de suporte!");
            }
        }

        [HttpGet]
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao consultar todas as permissões")]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", type: typeof(GenericErrorViewModel))]
        [Route("getAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return CustomResponse(await _applicationServiceRoles.GetAllAsync());
            }
            catch (Exception)
            {                
                return CustomResponseInternalError("Erro ao consultar todos as permissões. Por favor tente novamente, caso o erro persista contate a equipe de suporte");
            }
        }

        [HttpGet]
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao consultar a permissão")]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", type: typeof(GenericErrorViewModel))]
        [Route("getById")]
        public async Task<IActionResult> Get([FromHeader] int idRole)
        {
            try
            {
                if(idRole <= 0) return CustomResponse("Informe um código de permissao válido!");

                var role = await _applicationServiceRoles.GetByIdAsync(idRole);

                if(role.Name.IsNullOrEmpty())
                    AddErrorToStack("Dados da permissão não encontrado!");

                return CustomResponse(role);
            }
            catch (Exception)
            {                
                return CustomResponseInternalError("Erro ao consultar a permissão. Por favor tente novamente, caso o erro persista contate a equipe de suporte");
            }
        }
    }
}