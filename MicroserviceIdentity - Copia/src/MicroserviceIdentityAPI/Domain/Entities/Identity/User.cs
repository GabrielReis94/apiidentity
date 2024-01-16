using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MicroserviceIdentityAPI.Domain.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        [Column(TypeName = "varchar(50)")]
        public string Name {get; set;} = string.Empty;
        public int ClienteId { get; set; }

        public Cliente Clientes { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}