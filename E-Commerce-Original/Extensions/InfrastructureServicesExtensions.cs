using Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories;
using StackExchange.Redis;

namespace E_Commerce_Original.Extensions
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration) {

            
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlDefaultConnection"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>(_ =>

            ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!)

            );
            return services;
        } 
    }
}
