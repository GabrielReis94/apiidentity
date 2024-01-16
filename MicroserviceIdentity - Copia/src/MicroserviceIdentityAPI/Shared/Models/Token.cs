namespace MicroserviceIdentityAPI.Shared.Models
{
    public class Token
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; } = string.Empty;
        public string Expiration { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}