using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserRoles.Data;
using UserRoles.Models;
using UserRoles.Services;

/*
 * This initializes a new WebApplicationBuilder , which is responsible for configuring the web server, loading configuration settings, 
 * setting up logging, and registering necessary services. By default, the WebApplicationBuilder comes with several pre-configured settings to get you started quickly.
 * */
var builder = WebApplication.CreateBuilder(args);

/*
 * registers the essential Model-View-Controller (MVC) services into the ASP.NET Core dependency injection (DI) container. 
 * It enables your application to handle HTTP requests, bind incoming data to models, and render dynamic HTML views using the Razor engine.
 * */
// Add services to the container.
builder.Services.AddControllersWithViews();

/*
 * The lifetime of a DbContext begins when the instance is created and ends when the instance is disposed. A DbContext instance is designed to be used for a single unit-of-work. 
 * This means that the lifetime of a DbContext instance is usually very short.
 * Each HTTP request corresponds to a single unit-of-work. This makes tying the context lifetime to that of the request a good default for web applications.
 * ASP.NET Core applications are configured using dependency injection. EF Core can be added to this configuration using AddDbContext in Program.cs.
 * 
 * DbContext in dependency injection for ASP.NET Core:
 * The preceding code registers AppDbContext, a subclass of DbContext, as a scoped service in the ASP.NET Core app service provider. 
 * The service provider is also known as the dependency injection container. 
 * The context is configured to use the SQL Server database provider and reads the connection string from ASP.NET Core configuration.
 */
builder.Services.AddDbContext<AppDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/*
 * The code builder.Services.AddIdentity configures the ASP.NET Core Identity system, which manages user authentication, registration, passwords, roles, and tokens. 
 * It registers essential services like UserManager and SignInManager in your application's dependency injection container.
 * 
 * To use builder.Services.AddIdentity properly, you specify your custom IdentityUser and IdentityRole types, and chain it with the database context and token providers.
 * 
 * The AddIdentity method is used to configure the Identity system in an ASP.NET Core application. 
 * It sets up the necessary services for user authentication and authorization, including user and role management, password hashing, and token generation.
 * */
builder.Services.AddIdentity<Users, IdentityRole>
    (options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        }
    )
    /*
     * In ASP.NET Core, .AddEntityFrameworkStores<TContext>() connects your Identity system to your Entity Framework Core database. It automatically maps 
     * built-in identity interfaces to concrete database tables, enabling you to use services like UserManager and SignInManager to manage users and roles via SQL. 
     */
    .AddEntityFrameworkStores<AppDbContext>()
    /*
     * The .AddDefaultTokenProviders() method in ASP.NET Core Identity adds default token providers for generating tokens used in operations like password resets, 
     * email confirmations, and two-factor authentication. These token providers generate secure tokens that can be sent to users for verification purposes.
     * 
     * In ASP.NET Core Identity, AddDefaultTokenProviders() registers the built-in token generators required to secure sensitive user operations. 
     * It is commonly chained directly to your Identity configuration.
     */
    .AddDefaultTokenProviders();

/*
 * The AddAuthentication method in ASP.NET Core configures the authentication services for your application. 
 * It sets up the authentication middleware, which is responsible for validating user credentials and managing authentication cookies or tokens.
 * 
 * The AddCookie method configures cookie-based authentication, specifying how authentication cookies are created, validated, and managed. 
 * It allows you to customize cookie settings such as expiration time, login path, and security options.
 * 
 * In summary, AddAuthentication sets up the overall authentication framework, while AddCookie specifically configures cookie-based authentication behavior.
 * 
 * After Build(), you should no longer treat builder.Services, builder.Logging, builder.Configuration, or builder.WebHost as places to make startup changes for 
 * the app that was just built.
 * Nothing is listening for HTTP requests yet.
 * That happens later when you call app.Run() or app.StartAsync().
 * */
var app = builder.Build();

/*
 * The SeedDatabase method is an asynchronous operation that populates the database with initial data, such as default users, roles, or other essential records. 
 * It ensures that the application has the necessary data to function correctly upon startup.
 * 
 * The await keyword is used to asynchronously wait for the completion of the SeedDatabase method. This allows the application to continue executing other tasks while 
 * waiting for the seeding process to finish, improving responsiveness and performance.
 * 
 * The app.Services property provides access to the application's service provider, which manages dependency injection and service lifetimes. 
 * It allows you to resolve services and access registered dependencies within the application.
 * 
 *  await       : Pauses the execution of the startup thread until the seeding operation completes, preventing the application from serving requests prematurely
 * .SeedService : A dedicated, injectable class (or static helper) responsible for handling database interactions
 * .SeedDatabase: The specific method (usually named SeedAsync() or SeedDatabase()) that contains your logic for inserting the data.
 * */
await SeedService.SeedDatabase(app.Services);

/*
 * The app.Environment property provides information about the current hosting environment, such as whether the application is running in Development, Staging, or Production mode. 
 * It allows you to conditionally configure middleware and services based on the environment.
 * 
 * The IsDevelopment() method checks if the current hosting environment is set to Development. 
 * This is useful for enabling development-specific features, such as detailed error pages or debugging tools, while keeping them disabled in production for security and performance reasons.
 * 
 * Uses WebApplication.Environment to distinguish the environment.
 * Calls UseExceptionHandler, which adds Exception Handler Middleware to the request processing pipeline to handle exceptions.
 * Calls UseHsts, which adds HSTS Middleware to apply the Strict-Transport-Security header.
 */
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
/*
 * The UseHttpsRedirection method adds middleware to the request processing pipeline that automatically redirects HTTP requests to HTTPS. 
 * This ensures that all communication between the client and server is encrypted, enhancing security.
 * 
 * The UseRouting method adds routing middleware to the request processing pipeline. It enables the application to match incoming HTTP requests to the appropriate route handlers, 
 * such as controllers or Razor Pages, based on the defined route patterns.
 */
app.UseHttpsRedirection();

/*
 * The UseAuthentication method adds authentication middleware to the request processing pipeline. 
 * It enables the application to authenticate users based on the configured authentication schemes, such as cookies or JWT tokens.
 * 
 * The UseAuthorization method adds authorization middleware to the request processing pipeline. 
 * It ensures that authenticated users have the necessary permissions to access specific resources or perform certain actions based on their roles or claims.
 */
app.UseRouting();

/*
 * The UseStaticFiles method adds middleware to serve static files, such as images, CSS, and JavaScript, from the wwwroot folder. 
 * It allows clients to access these files directly without going through the MVC pipeline.
 */
app.UseAuthentication();

/*  * The UseAuthorization method adds authorization middleware to the request processing pipeline. 
 *    It ensures that authenticated users have the necessary permissions to access specific resources or perform certain actions based on their roles or claims.
 */
app.UseAuthorization();

/*
 * The MapStaticAssets method is a custom extension method that maps static assets to specific routes or endpoints in the application. 
 * It allows you to define how static files are served and accessed by clients, providing flexibility in organizing and delivering static content.
 */ 
app.MapStaticAssets();

/*
 * The MapControllerRoute method defines a route for the MVC controllers in the application. 
 * It specifies the URL pattern that maps to specific controller actions, allowing clients to access different parts of the application based on the defined routes.
 */
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

/*
 * The Run method starts the web application and begins listening for incoming HTTP requests. 
 * It blocks the calling thread until the application is shut down, allowing it to handle requests and respond to clients.
 */
app.Run();
