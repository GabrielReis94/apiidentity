using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Repositories;
using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Services;
using MicroserviceIdentityAPI.Domain.Domain.Services.Services.Base;
using MicroserviceIdentityAPI.Domain.Entities.Identity;

namespace MicroserviceIdentityAPI.Domain.Domain.Services.Services
{
    public class ServiceRole : ServiceBase<Role>, IServiceRole
    {
        private readonly IRepositoryRole _repositoryRole;

        public ServiceRole(IRepositoryRole repositoryRole)
            : base(repositoryRole)
        {
            _repositoryRole = repositoryRole;
        }

        public override async Task<bool> AddAsync(Role obj)
        {
            try
            {
                _repositoryRole.Add(obj);

                return await _repositoryRole.SaveChangesAsync();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public override async Task<List<Role>> GetAllAsync()
        {
            try
            {
                return await _repositoryRole.GetAllAsync();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public override async Task<Role> GetByIdAsync(int id)
        {
            try
            {
                return await _repositoryRole.GetByIdAsync(id);
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}