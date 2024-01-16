using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Repositories.Base;
using MicroserviceIdentityAPI.Infra.Data;
using MicroserviceIdentityAPI.Infra.Data.ConfigSqlData;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceIdentityAPI.Infra.Repository.Repositories.Base
{
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly IdentityContext _identityContext;

        public RepositoryBase(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public void Add(TEntity obj)
        {
            try
            {
                _identityContext.Set<TEntity>().Add(obj);
            }
            catch (Exception)
            {                
                throw new Exception("Erro ao cadastrar");
            }
        }              

        public async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                return await _identityContext.Set<TEntity>().ToListAsync();
            }
            catch (Exception)
            {                
                throw new Exception("Erro ao consultar");
            }
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                return await _identityContext.Set<TEntity>().FindAsync(id);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public void Update(TEntity obj)
        {
            try
            {
                _identityContext.Entry(obj).State = EntityState.Modified;
            }
            catch (Exception)
            {                
                throw new Exception("Erro ao atualizar");
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _identityContext.SaveChangesAsync()) > 0;
        }

        public void Dispose()
        {
            _identityContext.Dispose();
        }
    }
}