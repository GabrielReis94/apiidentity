using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using Microsoft.Extensions.Configuration.Json;

namespace MicroserviceIdentityAPI.Infra.Data.ConfigSqlData.Cryptography.Provider
{
    public class UnProtectedJsonConfigurationProvider : JsonConfigurationProvider
    {
        private readonly UnProtectedJsonConfigurationSource unProtectedSource;
        private readonly HashSet<string> encryptedKeys
                    = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        private static readonly byte[] encryptedPrefixBytes = Encoding.UTF8.GetBytes("!ENC!");

        public UnProtectedJsonConfigurationProvider(UnProtectedJsonConfigurationSource source) : base(source)
        {
            this.unProtectedSource = source as UnProtectedJsonConfigurationSource;
        }

        private bool IsEncrypted(string text)
        {
            if( text == null) { return false; }

            byte[] decodedBytes;
            try
            {
                decodedBytes = Convert.FromBase64String(text);
            }
            catch (FormatException)
            {                
                return false;
            }
             
            return decodedBytes.Length >= encryptedPrefixBytes.Length
                        && decodedBytes.AsSpan(0, encryptedPrefixBytes.Length).SequenceEqual(encryptedPrefixBytes);
        }

        public string DecryptedValue(string value)
        {
            string ret = string.Empty;

            if(!string.IsNullOrEmpty(value))
            {
                if(IsEncrypted(value))
                {
                    var unProtectedValueWithPrefix = Convert.FromBase64String(value);
                    var protectedValue = new byte[unProtectedValueWithPrefix.Length - encryptedPrefixBytes.Length];
                    Buffer.BlockCopy(unProtectedValueWithPrefix, encryptedPrefixBytes.Length, protectedValue, 0, protectedValue.Length);

                    var unprotectedValue = ProtectedData.Unprotect(protectedValue, this.unProtectedSource.Entropy, this.unProtectedSource.Scope);

                     ret = Encoding.UTF8.GetString(unprotectedValue);
                }
            }

            return ret;
        }
    }
}