using MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Interfaces;
using MicroserviceIdentityAPI.Domain.DTO;
using MicroserviceIdentityAPI.Domain.Entities;

namespace MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Map
{
    public class MapperCustomer : IMapperCustomer
    {
        private List<ClienteRespDTO> lstClientesDto = new();
        private ClienteRespDTO clienteDto = new();
        public Cliente MapperToEntity(ClienteDTO clienteDto)
        {
            try
            {                
                var cliente = new Cliente
                {
                    Nome = clienteDto.Nome,
                    CodigoCliente = clienteDto.CodigoCliente
                };

                return cliente;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public List<ClienteRespDTO> MapperToListDto(List<Cliente> lstClientes)
        {
            try
            {
                if(lstClientes != null)
                {
                    foreach (var item in lstClientes)
                    {
                        lstClientesDto.Add(new ClienteRespDTO
                        {
                            IdCliente = item.Id,
                            Nome = item.Nome,
                            CodigoCliente = item.CodigoCliente
                        });
                    }
                }

                return lstClientesDto;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public ClienteRespDTO MapperToDto(Cliente cliente)
        {
            try
            {
                if(cliente != null)
                {
                    clienteDto = new ClienteRespDTO
                    {
                        IdCliente = cliente.Id,
                        Nome = cliente.Nome,
                        CodigoCliente = cliente.CodigoCliente
                    };
                }               

                return clienteDto;
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}