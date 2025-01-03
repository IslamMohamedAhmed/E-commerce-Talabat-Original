using Services.Abstraction;
using Services;
using Contracts;
using Persistence.Repositories;
using Shared;

namespace E_Commerce_Original.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration) {

            services.AddAutoMapper(typeof(Services.MappingReference).Assembly);
            services.AddScoped<IServicesManager, ServicesManager>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}
