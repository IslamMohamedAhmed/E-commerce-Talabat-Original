using Contracts;
using E_Commerce_Original.Middlewares;
using System.Runtime.CompilerServices;

namespace E_Commerce_Original.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initializer.Initialize();
            await initializer.IdentityInitialize();
            return app;
        }
        public static WebApplication AddCustomMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionErrorHandlingMiddleware>();
            return app;
        }
    }
}
