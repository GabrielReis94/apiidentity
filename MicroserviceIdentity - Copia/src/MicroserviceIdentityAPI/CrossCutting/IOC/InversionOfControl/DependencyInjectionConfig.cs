using MicroserviceIdentityAPI.Application.Interfaces;
using MicroserviceIdentityAPI.Application.Services;
using MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Interfaces;
using MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Map;
using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Repositories;
using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Services;
using MicroserviceIdentityAPI.Domain.Domain.Services.Services;
using MicroserviceIdentityAPI.Infra.Repository.Repositories;

namespace MicroserviceIdentityAPI.CrossCutting.IOC.InversionOfControl
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection LoadDI(this IServiceCollection services)
        {
            #region Registers IOC

            #region IOC Applications
            services.AddScoped<IApplicationServiceCustomers, ApplicationServiceCustomers>();
            services.AddScoped<IApplicationServiceRoles, ApplicationServiceRoles>();
            #endregion

            #region IOC Services
            services.AddScoped<IServiceExample, ServiceExample>();
            services.AddScoped<IServiceCustomer, ServiceCustomers>();
            services.AddScoped<IServiceRole,ServiceRole>();
            #endregion

            #region  IOC Repositorys
            services.AddScoped<IRepositoryExample, RepositoryExample>();
            services.AddScoped<IRepositoryCustomers, RepositoryCustomers>();
            services.AddScoped<IRepositoryRole, RepositoryRoles>();
            #endregion

            #region IOC Mapper
            services.AddScoped<IMapperExample, MapperExample>();
            services.AddScoped<IMapperCustomer, MapperCustomer>();
            services.AddScoped<IMapperRole, MapperRole>();
            #endregion

            #region IOC Validates
            #endregion

            #region Factory
            #endregion

            #endregion

            return services;
        }
    }
}