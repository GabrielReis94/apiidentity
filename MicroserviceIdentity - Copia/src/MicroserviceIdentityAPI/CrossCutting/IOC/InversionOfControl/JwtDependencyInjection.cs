using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MicroserviceIdentityAPI.Infra.Data;
using MicroserviceIdentityAPI.Shared.Models;
using MicroserviceIdentityAPI.Shared.Security;
using MicroserviceIdentityAPI.Shared.TokenServices;

namespace MicroserviceIdentityAPI.CrossCutting.IOC.InversionOfControl
{
    public static class JwtDependencyInjection
    {
        public static IServiceCollection AddJwtDependency(this IServiceCollection services, TokenConfigurations tokenConfigurations)
        {
            services.AddSingleton(tokenConfigurations);

            services.AddIdentity<ApplicationUser,IdentityRole>()
                    .AddEntityFrameworkStores<ApiSecurityDbContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<AccessManager>();
            services.AddScoped<TokenService>();

            var signingConfigurations = new SigningConfigurations(tokenConfigurations);

            services.AddSingleton(signingConfigurations);

            services.AddTransient<IdentityInitializer>();

            services.AddAuthentication(authOpt => 
            {
                authOpt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOpt => 
            {
                var paramsValidation = bearerOpt.TokenValidationParameters;
                paramsValidation.IssuerSigningKey=signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;

                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth => 
            {
                auth.AddPolicy("Acesso-Api", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
             
            return services;
        }
    }
}