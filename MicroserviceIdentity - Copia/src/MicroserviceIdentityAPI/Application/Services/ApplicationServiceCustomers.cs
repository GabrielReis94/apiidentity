using MicroserviceIdentityAPI.Application.Interfaces;
using MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Interfaces;
using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Services;
using MicroserviceIdentityAPI.Domain.DTO;

namespace MicroserviceIdentityAPI.Application.Services
{
    public class ApplicationServiceCustomers : IApplicationServiceCustomers
    {
        private readonly IMapperCustomer _mapperCustomer;
        private readonly IServiceCustomer _serviceCustomer;

        public ApplicationServiceCustomers(IMapperCustomer mapperCustomer, IServiceCustomer serviceCustomer)
        {
            _mapperCustomer = mapperCustomer;
            _serviceCustomer = serviceCustomer;
        }

        public async Task<bool> AddAsync(ClienteDTO clienteDto)
        {
            try
            {
                var cliente = _mapperCustomer.MapperToEntity(clienteDto);

                return await _serviceCustomer.AddAsync(cliente);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public async Task<List<ClienteRespDTO>> GetAllAsync()
        {
            try
            {
               var lstClientes = await _serviceCustomer.GetAllAsync();

               return _mapperCustomer.MapperToListDto(lstClientes);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public async Task<ClienteRespDTO> GetByIdAsync(int id)
        {
            try
            {
                var cliente = await _serviceCustomer.GetByIdAsync(id);

                return _mapperCustomer.MapperToDto(cliente);
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}