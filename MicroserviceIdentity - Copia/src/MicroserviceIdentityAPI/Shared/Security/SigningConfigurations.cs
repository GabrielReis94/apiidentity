using System.Text;
using Microsoft.IdentityModel.Tokens;
using MicroserviceIdentityAPI.Shared.Models;

namespace MicroserviceIdentityAPI.Shared.Security
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get;}
        public SigningCredentials SigningCredentials { get;}

        public SigningConfigurations(TokenConfigurations tokenConfigurations)
        {
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.SecretKey));
            
            SigningCredentials = new (Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}