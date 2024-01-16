using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration.Json;

namespace MicroserviceIdentityAPI.Infra.Data.ConfigSqlData.Cryptography.Provider
{
    public class ProtectedJsonConfigurationSource : JsonConfigurationSource
    {
        internal byte[] Entropy { get; private set;}

        public ProtectedJsonConfigurationSource(byte[] entropy)
        {
            this.Entropy = entropy;
        }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new ProtectedJsonConfigurationProvider(this);
        }

        public DataProtectionScope Scope { get; set; }
        public IEnumerable<Regex> EncryptedKeyExpressions { get; set; }
    }
}