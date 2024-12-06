using Contracts;
using E_Commerce_Original.Extensions;
using E_Commerce_Original.Factories;
using E_Commerce_Original.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Services.Abstraction;

namespace E_Commerce_Original
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Services
            builder.Services.AddCoreServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Logging.Services.AddPresentationServices(); 
            #endregion


            var app = builder.Build();
            app.AddCustomMiddleware();
            await app.SeedDbAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();


        }
     


    }

}
