using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration.Json;

namespace MicroserviceIdentityAPI.Infra.Data.ConfigSqlData.Cryptography.Provider
{
    public class UnProtectedJsonConfigurationSource : JsonConfigurationSource
    {
        internal byte[] Entropy { get; private set; }
        public DataProtectionScope Scope { get; set; }
        public IEnumerable<Regex> EncryptedKeyExpressions { get; set; }

        public UnProtectedJsonConfigurationSource(byte[] entropy)
        {
            this.Entropy = entropy;
        }

        public string GetDescryptString(string value)
        {
            var provider = new UnProtectedJsonConfigurationProvider(this);

            string ret = provider.DecryptedValue(value);

            return ret;
        }
    }
}