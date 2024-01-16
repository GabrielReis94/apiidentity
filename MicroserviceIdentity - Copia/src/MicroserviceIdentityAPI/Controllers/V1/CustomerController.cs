using MicroserviceIdentityAPI.Application.Interfaces;
using MicroserviceIdentityAPI.Controllers.Base;
using MicroserviceIdentityAPI.Domain.Domain.Services.Extensions;
using MicroserviceIdentityAPI.Domain.DTO;
using MicroserviceIdentityAPI.Domain.Models;
using MicroserviceIdentityAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace MicroserviceIdentityAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    //[Authorize("Acesso-API")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class CustomerController : MainController
    {
        private readonly IApplicationServiceCustomers _applicationServiceCustomers;

        public CustomerController(IApplicationServiceCustomers applicationServiceCustomers)
        {
            _applicationServiceCustomers = applicationServiceCustomers;
        }

        [HttpPost]
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao registrar o cliente")]
        [SwaggerResponse(statusCode: 400, description: "Erro nos campos", type: typeof(ValidateInputViewModel))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", type: typeof(GenericErrorViewModel))]
        [CustomModelStateValidation]
        [Route("add")]
        public async Task<IActionResult> Post(ClienteDTO cliente)
        {
            try
            {   
                var result = await _applicationServiceCustomers.AddAsync(cliente);
                if(!result)
                    AddErrorToStack("Erro ao cadastrar o cliente. Por favor contate a equipe de suporte!");
                
                return CustomResponse("Cadastrado com sucesso", result);
                    
            }
            catch (Exception)
            {                
                return CustomResponseInternalError("Erro ao cadastrar o cliente. Por favor contate a equipe de suporte!");
            }
        }
        
        [HttpGet]
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao consultar todos os clientes")]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", type: typeof(GenericErrorViewModel))]
        [Route("getAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return CustomResponse(await _applicationServiceCustomers.GetAllAsync());
            }
            catch (Exception)
            {                
                return CustomResponseInternalError("Erro ao consultar todos os clientes. Por favor tente novamente, caso o erro persista contate a equipe de suporte");
            }
        }

        [HttpGet]
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao consultar o cliente")]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", type: typeof(GenericErrorViewModel))]
        [Route("getById")]
        public async Task<IActionResult> Get([FromHeader] int idCliente)
        {
            try
            {
                if(idCliente <= 0) return CustomResponse("Informe um código de cliente válido!");

                var cliente = await _applicationServiceCustomers.GetByIdAsync(idCliente);

                if(cliente.Nome.IsNullOrEmpty())
                    AddErrorToStack("Dados do cliente não encontrado!");

                return CustomResponse(cliente);
            }
            catch (Exception)
            {                
                return CustomResponseInternalError("Erro ao consultar o cliente. Por favor tente novamente, caso o erro persista contate a equipe de suporte");
            }
        }
    }
}