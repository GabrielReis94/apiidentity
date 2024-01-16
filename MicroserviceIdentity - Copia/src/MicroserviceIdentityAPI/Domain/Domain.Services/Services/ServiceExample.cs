using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Repositories;
using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Services;
using MicroserviceIdentityAPI.Domain.Domain.Services.Services.Base;
using MicroserviceIdentityAPI.Domain.Entities;

namespace MicroserviceIdentityAPI.Domain.Domain.Services.Services
{
    public class ServiceExample : ServiceBase<Example>, IServiceExample
    {
        private readonly IRepositoryExample _repository;

        public ServiceExample(IRepositoryExample repository)
                : base(repository)
        {
            _repository = repository;
        }

        public override async Task<bool> AddAsync(Example obj)
        {
            try
            {
                 _repository.Add(obj);

                 return await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public override Task<List<Example>> GetAllAsync()
        {
            try
            {
                return _repository.GetAllAsync();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public override Task<Example> GetByIdAsync(int id)
        {
            try
            {
                return _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        // public override async Task<bool> DeleteAsync(int id)
        // {
        //     try
        //     {
        //         _repository.Delete(id);

        //         return await _repository.
        //     }
        //     catch (Exception ex)
        //     {                
        //         throw new Exception(ex.Message);
        //     }
        // }

        public override async Task<bool> UpdateAsync(Example obj)
        {
            try
            {
                _repository.Update(obj);

                return await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
    }
}