using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Repositories.Base;
using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Services.Base;

namespace MicroserviceIdentityAPI.Domain.Domain.Services.Services.Base
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<bool> AddAsync(TEntity obj)
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

        // public virtual async Task<bool> DeleteAsync(int id)
        // {
        //     try
        //     {
        //         return await _repository.DeleteAsync(id);
        //     }
        //     catch (Exception ex)
        //     {                
        //         throw new Exception(ex.Message);
        //     }
        // }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {                                
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<bool> UpdateAsync(TEntity obj)
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