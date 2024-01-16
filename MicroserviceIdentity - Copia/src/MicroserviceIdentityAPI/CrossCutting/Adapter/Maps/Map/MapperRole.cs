using MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Interfaces;
using MicroserviceIdentityAPI.Domain.DTO;
using MicroserviceIdentityAPI.Domain.Entities.Identity;

namespace MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Map
{
    public class MapperRole : IMapperRole
    {
        private List<RoleRespDTO> lstRolesDto = new ();
        private RoleRespDTO roleDto = new();

        public Role MapperToEntity(RoleDTO roleDto)
        {
            try
            {                
                var role = new Role
                {
                   Name =   roleDto.Name,
                   NormalizedName = roleDto.NormalizedName   
                };

                return role;
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        public RoleRespDTO MapperToDto(Role role)
        {
            try
            {
                if(role != null)
                {
                    roleDto = new RoleRespDTO
                    {
                        IdRole = role.Id,
                        Name = role.Name,
                        NormalizedName = role.NormalizedName
                    };
                }               
                
                return roleDto;
            }
            catch (Exception)
            {                
                throw;
            }
        }        

        public List<RoleRespDTO> MapperToListDto(List<Role> lstRoles)
        {
            try
            {
                if(lstRoles != null)
                {
                    foreach (var item in lstRoles)
                    {
                        lstRolesDto.Add(new RoleRespDTO
                        {
                            IdRole = item.Id,
                            Name = item.Name,
                            NormalizedName = item.NormalizedName
                        });
                    }
                }

                return lstRolesDto;
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}