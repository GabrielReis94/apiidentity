using MicroserviceIdentityAPI.Shared.Models.Base;

namespace MicroserviceIdentityAPI.Shared.Models
{
    public class LogRequest : LogBase
    {
        public string Client { get; set; } = string.Empty;
        public object? Request { get; set; }
    }
}