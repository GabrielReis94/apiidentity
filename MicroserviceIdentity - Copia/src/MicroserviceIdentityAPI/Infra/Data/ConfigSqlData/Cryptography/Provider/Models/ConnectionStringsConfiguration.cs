using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace MicroserviceIdentityAPI.Infra.Data.ConfigSqlData.Cryptography.Provider.Models
{
    public class ConnectionStringsConfiguration
    {
        [JsonProperty("ConnectionStrings")]
        public string Connection { get; set; }
    }    
}