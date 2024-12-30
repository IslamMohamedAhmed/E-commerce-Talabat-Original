using Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistence.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext storeContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public DbInitializer(StoreContext storeContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.storeContext = storeContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task IdentityInitialize()
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!userManager.Users.Any())
            {
                var SuperAdminUser = new User
                {
                    DisplayName = "Super Admin User",
                    Email = "SuperAdminUser@gmail.com",
                    PhoneNumber = "1234567890",
                    UserName = "SuperAdminUser"
                };
                var AdminUser = new User
                {
                    DisplayName = "Admin User",
                    Email = "AdminUser@gmail.com",
                    PhoneNumber = "1234567890",
                    UserName = "AdminUser"
                };

                await userManager.CreateAsync(SuperAdminUser,"Passw0rd");
                await userManager.CreateAsync(AdminUser,"Passw0rd");


                await userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                await userManager.AddToRoleAsync(AdminUser, "Admin");
            }

        }

        public async Task Initialize()
        {
            try
            {
                if (storeContext.Database.GetPendingMigrations().Any())
                {
                    await storeContext.Database.MigrateAsync();
                    await storeContext.SaveChangesAsync();
                }

                if (!storeContext.ProductBrands.Any())
                {
                    var brands = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\brands.json");
                    var brandresult = JsonSerializer.Deserialize<List<ProductBrand>>(brands);
                    if (brandresult is not null && brandresult.Count > 0)
                    {
                        await storeContext.ProductBrands.AddRangeAsync(brandresult);
                        await storeContext.SaveChangesAsync();
                    }

                }
                if (!storeContext.ProductTypes.Any())
                {
                    var types = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\types.json");
                    var typeresult = JsonSerializer.Deserialize<List<ProductType>>(types);
                    if (typeresult is not null && typeresult.Count > 0)
                    {
                        await storeContext.ProductTypes.AddRangeAsync(typeresult);
                        await storeContext.SaveChangesAsync();
                    }

                }

                if (!storeContext.Products.Any())
                {
                    var Products = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\products.json");
                    var productresult = JsonSerializer.Deserialize<List<Product>>(Products);
                    if (productresult is not null && productresult.Count > 0)
                    {
                        await storeContext.Products.AddRangeAsync(productresult);
                        await storeContext.SaveChangesAsync();
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
