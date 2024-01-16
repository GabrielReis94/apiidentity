using Microsoft.AspNetCore.Identity;

namespace MicroserviceIdentityAPI.Domain.Entities.Identity
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}