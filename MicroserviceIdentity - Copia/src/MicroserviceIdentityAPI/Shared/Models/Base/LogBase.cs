namespace MicroserviceIdentityAPI.Shared.Models.Base
{
    public abstract class LogBase
    {
        public Guid Id { get; set; }
        public DateTime LogDate { get; set; }
        public string Method { get; set; } = string.Empty;
        public string SearchKey { get; set; } = string.Empty;
    }
}