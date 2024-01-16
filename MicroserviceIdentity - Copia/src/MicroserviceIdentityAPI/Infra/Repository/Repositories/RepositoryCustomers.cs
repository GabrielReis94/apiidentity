using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Repositories;
using MicroserviceIdentityAPI.Domain.Entities;
using MicroserviceIdentityAPI.Infra.Data;
using MicroserviceIdentityAPI.Infra.Repository.Repositories.Base;

namespace MicroserviceIdentityAPI.Infra.Repository.Repositories
{
    public class RepositoryCustomers : RepositoryBase<Cliente>, IRepositoryCustomers
    {
        private readonly IdentityContext _identityContext;

        public RepositoryCustomers(IdentityContext identityContext)
                : base(identityContext)
        {
            _identityContext = identityContext;
        }
    }
}