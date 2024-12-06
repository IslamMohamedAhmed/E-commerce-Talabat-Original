using Services.Abstraction;
using Services;

namespace E_Commerce_Original.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services) {

            services.AddAutoMapper(typeof(Services.MappingReference).Assembly);
            services.AddScoped<IServicesManager, ServicesManager>();
            return services;
        }
    }
}
