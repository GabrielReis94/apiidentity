using MicroserviceIdentityAPI.Domain.Entities.Base;

namespace MicroserviceIdentityAPI.Domain.Entities
{
    public class Example : Entity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime RegisterDate { get; set; }
    }
}