using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using MicroserviceIdentityAPI.Shared.Models;
using MicroserviceIdentityAPI.Shared.Security;

namespace MicroserviceIdentityAPI.Shared.TokenServices
{
    public class TokenService
    {
        private TokenConfigurations _tokenConfigurations;
        private SigningConfigurations _signingConfigurations;
        private IDistributedCache _cache;

        public TokenService(
            TokenConfigurations tokenConfigurations,
            SigningConfigurations signingConfigurations,
            IDistributedCache cache)
        {
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
            _cache = cache;
        }

        public Token GenerateToken(AccessCredentials credentials)
        {
            ClaimsIdentity identity = new (
                new GenericIdentity(credentials.UserName, "Login"),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName,credentials.UserName)
                });

            DateTime creationDate = DateTime.Now;
            DateTime expirationDate = creationDate + TimeSpan.FromHours(_tokenConfigurations.ValidateHoursToken);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            Token result = new()
            {
                Authenticated = true,
                Created = creationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                RefreshToken = Guid.NewGuid().ToString().Replace("-",string.Empty),
                Message = "OK"
            };

            RefreshTokenData refreshTokenData = new()
            {
                RefreshToken = result.RefreshToken,
                UserName = credentials.UserName
            };

            TimeSpan finalExpiration = 
                TimeSpan.FromHours(_tokenConfigurations.FinalExpirationRefresh);
            
            DistributedCacheEntryOptions optCach = 
                new DistributedCacheEntryOptions();
            
            // optCach.SetAbsoluteExpiration(finalExpiration);
            // _cache.SetString(result.RefreshToken, JsonSerializer.Serialize(refreshTokenData),optCach);

            return result;
        }
    }
}