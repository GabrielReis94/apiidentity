using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Repositories;
using MicroserviceIdentityAPI.Domain.Entities;
using MicroserviceIdentityAPI.Infra.Data;
using MicroserviceIdentityAPI.Infra.Repository.Repositories.Base;

namespace MicroserviceIdentityAPI.Infra.Repository.Repositories
{
    public class RepositoryExample : RepositoryBase<Example>, IRepositoryExample
    {
        private readonly IdentityContext _identityContext;

        public RepositoryExample(IdentityContext identityContext)
                : base(identityContext)
        {
            _identityContext = identityContext;
        }
    }
}