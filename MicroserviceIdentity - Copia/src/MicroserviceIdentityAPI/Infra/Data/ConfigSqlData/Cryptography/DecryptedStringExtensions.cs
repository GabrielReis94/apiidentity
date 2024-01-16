using System.Text.RegularExpressions;
using MicroserviceIdentityAPI.Infra.Data.ConfigSqlData.Cryptography.Provider;

namespace MicroserviceIdentityAPI.Infra.Data.ConfigSqlData.Cryptography
{
    public static class DecryptedStringExtensions
    {
        public static string GetDecryptString(string value)
        {
            Regex[] encryptedKeyExpressions = new Regex[] { new ("ConnectionStrings") };

            var source = new UnProtectedJsonConfigurationSource(new byte[] { 10, 15, 18, 5, 3, 2, 9 })
            {
                Path = string.Empty,
                Optional = true,
                EncryptedKeyExpressions = encryptedKeyExpressions
            };

            var decryptedValue = source.GetDescryptString(value);

            return decryptedValue;
        }
    }
}