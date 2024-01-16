using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using MicroserviceIdentityAPI.Shared.Models;

namespace MicroserviceIdentityAPI.Shared.Security
{
    public class AccessManager
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        private IDistributedCache _cache;

        public AccessManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations,
            IDistributedCache cache)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _cache = cache;
        }

        public bool ValidateCredentials(AccessCredentials credentials)
        {
            bool validCredentials = false;

            if(credentials != null && !string.IsNullOrWhiteSpace(credentials.UserName))
            {
                if(credentials.GrantType.ToLower() == "password")
                {
                    var userIdentity = _userManager.FindByNameAsync(credentials.UserName).Result;

                    if(userIdentity != null)
                    {
                        var resultLogin = _signInManager
                                .CheckPasswordSignInAsync(userIdentity, credentials.Password, false)
                                .Result;
                        if(resultLogin.Succeeded)
                        {
                            validCredentials = _userManager.IsInRoleAsync(
                                    userIdentity, _tokenConfigurations.AccessRole).Result;
                        }
                    }
                }
                else if (credentials.GrantType.ToLower() == "refresh_token")
                {
                   if(!string.IsNullOrWhiteSpace(credentials.RefreshToken))
                   {
                        RefreshTokenData? refreshTokenBase = null;

                        string strTokenArmazenado = 
                            _cache.GetString(credentials.RefreshToken);

                        if(!string.IsNullOrWhiteSpace(strTokenArmazenado))
                        {
                            refreshTokenBase = JsonSerializer
                                    .Deserialize<RefreshTokenData>(strTokenArmazenado);
                        }

                        validCredentials = (refreshTokenBase != null && 
                            credentials.UserName == refreshTokenBase.UserName &&
                            credentials.RefreshToken == refreshTokenBase.RefreshToken);
                        
                        if(validCredentials)
                            _cache.Remove(credentials.RefreshToken);
                   }
                }
            }

            return validCredentials;
        }
    }
}