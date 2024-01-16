using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Repositories;
using MicroserviceIdentityAPI.Domain.Entities.Identity;
using MicroserviceIdentityAPI.Infra.Data;
using MicroserviceIdentityAPI.Infra.Repository.Repositories.Base;

namespace MicroserviceIdentityAPI.Infra.Repository.Repositories
{
    public class RepositoryRoles : RepositoryBase<Role>, IRepositoryRole
    {
        private readonly IdentityContext _identityContext;

        public RepositoryRoles(IdentityContext identityContext)
            : base(identityContext)
        {
            _identityContext = identityContext;
        }
    }
}