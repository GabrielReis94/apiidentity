using MicroserviceIdentityAPI.Shared.Models.Base;

namespace MicroserviceIdentityAPI.Shared.Models
{
    public class LogResponse : LogBase
    {
        public object? Response { get; set; }
    }
}