using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MicroserviceIdentityAPI.Infra.Data.ConfigSqlData.Cryptography.Provider
{
    public partial class ProtectedJsonConfigurationProvider : JsonConfigurationProvider
    {
        private readonly ProtectedJsonConfigurationSource protectedSource;

        private readonly HashSet<string> encryptedKeys 
                    = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        
        private static readonly byte[] encryptedPrefixBytes = Encoding.UTF8.GetBytes("!ENC!");

        private bool IsEncrypted(string text)
        {
            if (text == null) { return false; }

            byte[] decodedBytes;
            try
            {
                decodedBytes = Convert.FromBase64String(text);
            }
            catch (Exception)
            {                
                return false;
            }

            return decodedBytes.Length >= encryptedPrefixBytes.Length
                        && decodedBytes.AsSpan(0, encryptedPrefixBytes.Length).SequenceEqual(encryptedPrefixBytes);
        }

        private string ConvertToTokenPath(string key)
        {
            var jsonStringBuilder = new StringBuilder();

            var keyParts = key.Split(":");

            for (int keyPartIndex = 0; keyPartIndex < keyParts.Length; keyPartIndex++)
            {
                var keyPart = keyParts[keyPartIndex];

                if(keyPart.All(char.IsDigit))
                {
                    jsonStringBuilder.Append('[').Append(keyPart).Append(']');
                }
                else if (keyPartIndex > 0)
                {
                    jsonStringBuilder.Append('.').Append(keyPart);
                }
                else
                {
                    jsonStringBuilder.Append(keyPart);
                }
            }

            return jsonStringBuilder.ToString();
        }

        private void WriteValues(IDictionary<string, string> encryptedKeyValues)
        {
            try
            {
                if (encryptedKeyValues == null || encryptedKeyValues.Count == 0)
                {
                    return;
                }

                using(var stream = new FileStream(this.protectedSource.Path, FileMode.Open, FileAccess.ReadWrite))
                {
                    JObject json;

                    using(var streamReader = new StreamReader(stream, Encoding.UTF8, true, 4096, true))
                    {
                        using(var jsonTextReader = new JsonTextReader(streamReader))
                        {
                            json = JObject.Load(jsonTextReader);

                            foreach (var encryptedKeyValue in encryptedKeyValues)
                            {
                                var tokenPath = this.ConvertToTokenPath(encryptedKeyValue.Key);
                                var value = json.SelectToken(tokenPath) as JValue;

                                if( value?.Value != null)
                                {
                                    value.Value = encryptedKeyValue.Value;
                                }
                            }
                        }
                    }
                    
                    stream.Seek(0, SeekOrigin.Begin);
                    using(var streamWriter = new StreamWriter(stream))
                    {
                        using(var jsonTextWriter = new JsonTextWriter(streamWriter) { Formatting =  Formatting.Indented })
                        {
                            json.WriteTo(jsonTextWriter);
                        }
                    }
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(string.Format(this.protectedSource.Path + " " + encryptedKeyValues.ToArray().Count(),  ex));
            }
        }

        public ProtectedJsonConfigurationProvider(ProtectedJsonConfigurationSource source) 
                : base(source)
        {
            this.protectedSource = source as ProtectedJsonConfigurationSource;
        }

        public override void Load(Stream stream)
        {
            base.Load(stream);

            var expressions = protectedSource.EncryptedKeyExpressions;

            if(expressions != null)
            {
                var encryptedKeyValuesToWrite = new Dictionary<string, string>();

                var keys = new string[this.Data.Keys.Count];

                this.Data.Keys.CopyTo(keys, 0);

                foreach (var key in keys)
                {
                    var value = this.Data[key];

                    if(!string.IsNullOrEmpty(value) && expressions.Any(e => e.IsMatch(key)))
                    {
                        this.encryptedKeys.Add(key);

                        if(!this.IsEncrypted(value))
                        {
                            var protectedValue = ProtectedData.Protect(Encoding.UTF8.GetBytes(value), protectedSource.Entropy, protectedSource.Scope);
                            var protectedValueWithPrefix = new List<byte>(encryptedPrefixBytes);
                            protectedValueWithPrefix.AddRange(protectedValue);

                            var protectedBase64Value = Convert.ToBase64String(protectedValueWithPrefix.ToArray());
                            encryptedKeyValuesToWrite.Add(key, protectedBase64Value);
                            this.Data[key] = protectedBase64Value;
                        }
                    }
                }

                this.WriteValues(encryptedKeyValuesToWrite);
            }
        }

        public override bool TryGet(string key, out string? value)
        {
            if(!base.TryGet(key, out value))
            {
                return false;
            }
            else if(!this.encryptedKeys.Contains(key))
            {
                return true;
            }

            var protectedValueWithPrefix = Convert.FromBase64String(value);
            var protectedValue = new byte[protectedValueWithPrefix.Length - encryptedPrefixBytes.Length];
            Buffer.BlockCopy(protectedValueWithPrefix, encryptedPrefixBytes.Length, protectedValue, 0, protectedValue.Length);

            var unProtectedValue = ProtectedData.Unprotect(protectedValue, this.protectedSource.Entropy, this.protectedSource.Scope);
            value = Encoding.UTF8.GetString(unProtectedValue);

            return true;
        }
    }
}