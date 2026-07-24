using Microsoft.AspNetCore.Identity;
using UserRoles.Data;
using UserRoles.Models;

namespace UserRoles.Services
{
    /*
     * The SeedService class is a utility service designed to initialize and populate the database with essential data, such as roles and an admin user. 
     * It ensures that the application has the necessary roles and a default administrative account upon startup.
     * 
     * The SeedDatabase method is the core of this service. It performs several key tasks:
     * 1. Ensures that the database is created and ready for use.
     * 2. Seeds predefined roles (e.g., "Admin" and "User") into the database if they do not already exist.
     * 3. Creates a default admin user with a specified email and password, assigning them the "Admin" role.
     * 
     * This service is typically invoked during application startup to guarantee that the application has the required initial data for proper functionality.
     */
    public class SeedService
    {
        /*
         * The SeedDatabase method is responsible for seeding the database with initial data. 
         * It creates a scope to access the required services, ensures the database is created, adds predefined roles, and creates an admin user if it does not already exist.
         * 
         * @param serviceProvider An IServiceProvider instance used to resolve required services such as AppDbContext, RoleManager, UserManager, and ILogger.
         */
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                // Ensure the database is ready
                logger.LogInformation("Ensuring the database is created.");
                await context.Database.EnsureCreatedAsync();

                // Add roles
                logger.LogInformation("Seeding roles.");
                await AddRoleAsync(roleManager, "Admin");
                await AddRoleAsync(roleManager, "User");

                // Add admin user
                logger.LogInformation("Seeding admin user.");
               // var adminEmail = "admin@codehub.com";
               var adminPassword = "Admin@123"; // Ensure this meets your password policy
                var adminEmail = "admin@UserRolesApplication.com";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new Users
                    {
                        FullName = "Admin UserRole Application",
                        UserName = adminEmail,
                        NormalizedUserName = adminEmail.ToUpper(),
                        Email = adminEmail,
                        NormalizedEmail = adminEmail.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        logger.LogInformation("Assigning Admin role to the admin user.");
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {
                        logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");

            }

        }

        /*
         * The AddRoleAsync method checks if a role exists in the database, and if it does not, it creates the role using the provided RoleManager. 
         * If the role creation fails, it throws an exception with detailed error information.
         * 
         * @param roleManager An instance of RoleManager<IdentityRole> used to manage roles in the identity system.
         * @param roleName The name of the role to be added.
         */
        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
