namespace MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Services.Base
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
         Task<bool> AddAsync(TEntity obj);
         Task<TEntity> GetByIdAsync(int id);
         Task<List<TEntity>> GetAllAsync();
         Task<bool> UpdateAsync(TEntity obj);         
    }
}