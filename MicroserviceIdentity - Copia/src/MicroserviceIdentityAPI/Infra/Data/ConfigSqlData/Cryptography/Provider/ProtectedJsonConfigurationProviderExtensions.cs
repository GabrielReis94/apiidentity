using System.Text.RegularExpressions;

namespace MicroserviceIdentityAPI.Infra.Data.ConfigSqlData.Cryptography.Provider
{
    public static class ProtectedJsonConfigurationProviderExtensions
    {
        public static IConfigurationBuilder AddProtectedJsonFile(this IConfigurationBuilder configurationBuilder
                                            , string path
                                            , bool optional
                                            , byte[] entropy
                                            , params Regex[] encryptedKeyExpressions)
        {
            var source = new ProtectedJsonConfigurationSource(entropy)
            {
                Path = path,
                Optional = optional,
                EncryptedKeyExpressions = encryptedKeyExpressions
            };

            return configurationBuilder.Add(source);
        }
    }
}