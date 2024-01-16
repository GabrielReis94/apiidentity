using MicroserviceIdentityAPI.Domain.DTO;
using MicroserviceIdentityAPI.Domain.Entities;

namespace MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Interfaces
{
    public interface IMapperCustomer
    {
         Cliente MapperToEntity(ClienteDTO clienteDto);
         List<ClienteRespDTO> MapperToListDto(List<Cliente> lstClientes);
         ClienteRespDTO MapperToDto(Cliente cliente);
    }
}