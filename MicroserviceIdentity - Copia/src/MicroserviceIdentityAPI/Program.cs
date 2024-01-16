using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MicroserviceIdentityAPI.CrossCutting.IOC.ConfigSerilog;
using MicroserviceIdentityAPI.CrossCutting.IOC.InversionOfControl;
using MicroserviceIdentityAPI.Domain.Entities.Identity;
using MicroserviceIdentityAPI.Infra.Data;
using MicroserviceIdentityAPI.Infra.Data.ConfigSqlData.Cryptography;
using MicroserviceIdentityAPI.Infra.Data.ConfigSqlData.Cryptography.Provider;
using MicroserviceIdentityAPI.Infra.Data.ConfigSqlData.Cryptography.Provider.Models;
using MicroserviceIdentityAPI.Shared.Models;
using MicroserviceIdentityAPI.Shared.Security;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

SerilogConfiguration.AddSerilogApi(builder.Configuration);
builder.Host.UseSerilog(Log.Logger);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var configurationBuilder = new ConfigurationBuilder()
            .AddProtectedJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, new byte[] { 10, 15, 18, 5, 3, 2, 9 },
            new Regex("ConnectionStrings"));

var configuration = configurationBuilder.Build();
//var connectionStringsConfiguration = configuration.GetSection("ConnectionStrings").Get<ConnectionStringsConfiguration>();

//var connection = connectionStringsConfiguration.Connection;

var connectionIdentity = builder.Configuration.GetConnectionString("IdentityConnection") ?? string.Empty;
builder.Services.AddDbContext<IdentityContext>(opt => opt.UseSqlServer(DecryptedStringExtensions.GetDecryptString(connectionIdentity)));

IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<User>(opt => 
{
    opt.Password.RequireDigit = true;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequiredLength = 10;
});

identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(Role), identityBuilder.Services);
identityBuilder.AddEntityFrameworkStores<IdentityContext>();
identityBuilder.AddRoleValidator<RoleValidator<Role>>();
identityBuilder.AddRoleManager<RoleManager<Role>>();
identityBuilder.AddSignInManager<SignInManager<User>>();

builder.Services.AddDistributedRedisCache(opt => 
{
    opt.Configuration = builder.Configuration.GetConnectionString("ConexaoRedis");
    opt.InstanceName = "APITemplate";
});

builder.Services.AddDbContext<ApiSecurityDbContext>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));

var tokenConfigurations = new TokenConfigurations();

new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
            .Configure(tokenConfigurations);

builder.Services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                })
                .ConfigureApiBehaviorOptions(opt => 
                {
                    opt.SuppressModelStateInvalidFilter = true;
                });

builder.Services.AddJwtDependency(tokenConfigurations);
builder.Services.AddSwaggerVersioning();
builder.Services.AddSwaggerVersionedApiExplorer();
builder.Services.AddSwaggerServiceDependency();
builder.Services.AddSwaggerDependency();

builder.Services.LoadDI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDependency(app.Services.GetRequiredService<IApiVersionDescriptionProvider>());
}

using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<IdentityInitializer>().Initialize();

// var identityInitializer = app.Services.GetRequiredService<IdentityInitializer>();

app.UseHttpsRedirection();
//identityInitializer?.Initialize();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
