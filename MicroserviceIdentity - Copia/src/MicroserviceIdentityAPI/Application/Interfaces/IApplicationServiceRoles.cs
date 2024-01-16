using MicroserviceIdentityAPI.Domain.DTO;

namespace MicroserviceIdentityAPI.Application.Interfaces
{
    public interface IApplicationServiceRoles
    {
         Task<bool> AddAsync(RoleDTO roleDto);
         Task<List<RoleRespDTO>> GetAllAsync();
         Task<RoleRespDTO> GetByIdAsync(int id);
    }
}