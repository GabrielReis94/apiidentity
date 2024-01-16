using MicroserviceIdentityAPI.Shared.Models.Base;

namespace MicroserviceIdentityAPI.Shared.Models
{
    public class LogError : LogBase
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public string InnerException { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
    }
}