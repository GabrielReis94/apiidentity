using Microsoft.AspNetCore.Identity;
using MicroserviceIdentityAPI.Infra.Data;
using MicroserviceIdentityAPI.Shared.Models;

namespace MicroserviceIdentityAPI.Shared.Security
{
    public class IdentityInitializer
    {
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly ApiSecurityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            TokenConfigurations tokenConfigurations,
            ApiSecurityDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _tokenConfigurations = tokenConfigurations;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if(_context.Database.EnsureCreated())
            {
                if(!_roleManager.RoleExistsAsync(_tokenConfigurations.AccessRole).Result)
                {
                    var result = _roleManager.CreateAsync(
                        new IdentityRole(_tokenConfigurations.AccessRole)).Result;

                    if(!result.Succeeded)
                    {
                        throw new Exception($"Erro durante a criação da role {_tokenConfigurations.AccessRole}.");
                    }
                }

                CreateUser(
                    new ApplicationUser
                    {
                        UserName = "desenvolvimento",
                        Email = "dev@dev.com",
                        EmailConfirmed = true
                    }, "D3$3nv0lv!m3nt0", _tokenConfigurations.AccessRole
                );

                CreateUser(
                    new ApplicationUser
                    {
                        UserName = "homologacao",
                        Email = "homolog@homologacao.com",
                        EmailConfirmed = true
                    }, "H0m0l@g@c@o", _tokenConfigurations.AccessRole
                );
            }
        }

        private void CreateUser(
            ApplicationUser user,
            string password,
            string initialRole = "")
        {
            if(_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var result = _userManager.CreateAsync(user, password).Result;

                if(result.Succeeded &&
                    !string.IsNullOrWhiteSpace(initialRole))
                {
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
                }
            }
        }
    }
}