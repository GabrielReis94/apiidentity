using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Repositories;
using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Services;
using MicroserviceIdentityAPI.Domain.Domain.Services.Services.Base;
using MicroserviceIdentityAPI.Domain.Entities;

namespace MicroserviceIdentityAPI.Domain.Domain.Services.Services
{
    public class ServiceCustomers : ServiceBase<Cliente>, IServiceCustomer
    {
        private readonly IRepositoryCustomers _repositoryCustomers;

        public ServiceCustomers(IRepositoryCustomers repositoryCustomers)
                : base (repositoryCustomers)
        {
            _repositoryCustomers = repositoryCustomers;
        }

        public override async Task<bool> AddAsync(Cliente obj)
        {
            try
            {
                _repositoryCustomers.Add(obj);

                return await _repositoryCustomers.SaveChangesAsync();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public override async Task<List<Cliente>> GetAllAsync()
        {
            try
            {
                return await _repositoryCustomers.GetAllAsync();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public override async Task<Cliente> GetByIdAsync(int id)
        {
            try
            {
                return await _repositoryCustomers.GetByIdAsync(id);
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}