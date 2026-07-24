using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserRoles.Models;

namespace UserRoles.Data
{
    /*
     * The AppDbContext class is a custom implementation of the IdentityDbContext class, which is part of the ASP.NET Core Identity framework. 
     * It serves as the primary bridge between your application and the underlying database, enabling you to perform CRUD (Create, Read, Update, Delete) operations on user data.
     * 
     * By inheriting from IdentityDbContext<Users>, the AppDbContext class gains access to all the built-in functionality provided by ASP.NET Core Identity for managing users, roles, and claims. 
     * The generic parameter <Users> specifies that this context will work with a custom user entity named Users, which extends the default IdentityUser class.
     */
    public class AppDbContext : IdentityDbContext<Users>
    {
        /*
         * The constructor for the AppDbContext class takes a DbContextOptions parameter, which is used to configure the context's behavior and connection to the database. 
         * It passes these options to the base IdentityDbContext class, allowing it to initialize the context with the specified settings.
         * 
         * @param options An instance of DbContextOptions that contains configuration information for the context, such as the database provider, connection string, and other settings.
         */
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
