namespace MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
         void Add(TEntity obj);
         Task<TEntity> GetByIdAsync(int id);
         Task<List<TEntity>> GetAllAsync();
         void Update(TEntity obj);
         Task<bool> SaveChangesAsync();         
    }
}