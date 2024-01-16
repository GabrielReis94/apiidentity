namespace MicroserviceIdentityAPI.Shared.Models
{
    public class RefreshTokenData
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}