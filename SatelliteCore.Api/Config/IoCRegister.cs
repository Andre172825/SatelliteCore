using Microsoft.Extensions.DependencyInjection;
using SatelliteCore.Api.Models.Config;
using SatelliteCore.Api.Services;
using SatelliteCore.Api.Services.Contracts;
using SatelliteCore.Api.DataAccess.Contracts.Repository;
using SatelliteCore.Api.DataAccess.Repository;
namespace SatelliteCore.Api.Config
{
    public static class IoCRegister
    {
        public static IServiceCollection AddRegistration(this IServiceCollection service)
        {
            AddRegisterRepositories(service);
            AddRegisterServices(service);
            AddRegisterOthers(service);
            return service;
        }
        private static IServiceCollection AddRegisterRepositories(this IServiceCollection service)
        {
            service.AddScoped<IUsuarioRepository, UsuarioRepository>();
            service.AddScoped<IAccesosRepository, AccesosRepository>();
            service.AddScoped<ICommonRepository, CommonRepository>();
            service.AddScoped<IPronosticoRepository, PronosticoRepository>();
            service.AddScoped<IControlCalidadRepository, ControlCalidadRepository>();
            service.AddScoped<IComercialRepository, ComercialRepository>();

            return service;
        }
        private static IServiceCollection AddRegisterServices(this IServiceCollection service)
        {
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IAuthServices, AuthServices>();
            service.AddScoped<IValidacionesServices, ValidacionesServices>();
            service.AddScoped<ICommonServices, CommonServices>();
            service.AddScoped<IPronosticoServices, PronosticoServices>();
            service.AddScoped<IControlCalidadServices, ControlCalidadServices>();
            service.AddScoped<IComercialServices, ComercialServices>();
            return service;
        }

        private static IServiceCollection AddRegisterOthers(this IServiceCollection service)
        {
            service.AddScoped<IAppConfig, AppConfig>();
            return service;
        }
    }
}
