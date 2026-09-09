using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    //Stap 23 :Add AddViewLocalization() to the service container to enable view localization.
    .AddViewLocalization();

//Step 24:Define folder where the localization resources are stored.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//Step 25: Define the supported cultures and set the default culture to English (United Kingdom) along with French,German, Hindi and Bengali (India).
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-GB", "fr-FR","DE-de", "hi-IN", "bn-IN" };
    options.SetDefaultCulture("en-GB")
           .AddSupportedCultures(supportedCultures)
           .AddSupportedUICultures(supportedCultures);
});

/*
 * STEP 5: Add the ApplicationDBContext to the service container and configure it to use SQL Server with the connection string from appsettings.json.
 */
builder.Services.AddDbContext<BestStoreMVC.Services.ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Step 26: Add the UseRequestLocalization middleware to the request pipeline to enable localization.
app.UseRequestLocalization(app.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>().Value);



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
