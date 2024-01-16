using MicroserviceIdentityAPI.Domain.DTO;

namespace MicroserviceIdentityAPI.Application.Interfaces
{
    public interface IApplicationServiceCustomers
    {
         Task<bool> AddAsync(ClienteDTO clienteDto);
         Task<List<ClienteRespDTO>> GetAllAsync();
         Task<ClienteRespDTO> GetByIdAsync(int id);
    }
}