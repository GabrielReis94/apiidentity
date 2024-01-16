using System.ComponentModel.DataAnnotations.Schema;
using MicroserviceIdentityAPI.Domain.Entities.Base;
using MicroserviceIdentityAPI.Domain.Entities.Identity;

namespace MicroserviceIdentityAPI.Domain.Entities
{
    public class Cliente : Entity
    {
        [Column(TypeName = "varchar(50)")]
        public string Nome { get; set; } = string.Empty;
        [Column(TypeName = "varchar(50)")]
        public string? CodigoCliente { get; set; }

        public ICollection<User> Users { get; set; }
    }
}