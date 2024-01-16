namespace MicroserviceIdentityAPI.Shared.Models
{
    public class TokenConfigurations
    {
        public string AccessRole { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public int ValidateHoursToken { get; set; }
        public int FinalExpirationRefresh { get; set; }
    }
}