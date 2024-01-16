using MicroserviceIdentityAPI.Application.Interfaces;
using MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Interfaces;
using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Services;
using MicroserviceIdentityAPI.Domain.DTO;

namespace MicroserviceIdentityAPI.Application.Services
{
    public class ApplicationServiceRoles : IApplicationServiceRoles
    {
        private readonly IMapperRole _mapperRole;
        private readonly IServiceRole _serviceRole;

        public ApplicationServiceRoles(IServiceRole serviceRole, IMapperRole mapperRole)
        {
            _serviceRole = serviceRole;
            _mapperRole = mapperRole;
        }

        public async Task<bool> AddAsync(RoleDTO roleDto)
        {
            try
            {
                var role = _mapperRole.MapperToEntity(roleDto);

                return await _serviceRole.AddAsync(role);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public async Task<List<RoleRespDTO>> GetAllAsync()
        {
            try
            {
                var lstRoles = await _serviceRole.GetAllAsync();

                return _mapperRole.MapperToListDto(lstRoles);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public async Task<RoleRespDTO> GetByIdAsync(int id)
        {
            try
            {
                var role = await _serviceRole.GetByIdAsync(id);

                return _mapperRole.MapperToDto(role);
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}