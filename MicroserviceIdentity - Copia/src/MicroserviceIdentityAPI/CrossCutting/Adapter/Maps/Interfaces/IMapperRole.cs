using MicroserviceIdentityAPI.Domain.DTO;
using MicroserviceIdentityAPI.Domain.Entities.Identity;

namespace MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Interfaces
{
    public interface IMapperRole
    {
         Role MapperToEntity(RoleDTO roleDto);
         List<RoleRespDTO> MapperToListDto(List<Role> lstRoles);
         RoleRespDTO MapperToDto(Role role);
    }
}