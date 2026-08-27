using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

//Step 10: Start: Configure Serilog to log the events  in the SQL Server database for a specified columns.
var columnOptions = new ColumnOptions();
columnOptions.Store.Remove(StandardColumn.Properties);
//columnOptions.Store.Add(StandardColumn.Id);
//columnOptions.Store.Add(StandardColumn.Message);
columnOptions.Store.Add(StandardColumn.LogEvent);
//columnOptions.Store.Add(StandardColumn.TimeStamp);

//Step 03:Start:Load our configuration (Serilogsettings.json) file
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("Serilogsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();
//Step 03:End

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
             .Enrich.FromLogContext()
             .Enrich.WithMachineName()
             .Enrich.WithEnvironmentName()
    .WriteTo.MSSqlServer(
    connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
    sinkOptions: new MSSqlServerSinkOptions
    {
        AutoCreateSqlDatabase = true,
        TableName = "CoreMVCLoggingLogs",
        AutoCreateSqlTable = true
    },
    columnOptions: columnOptions)
    .CreateLogger();
//Step 10: End



//Step 04:Start:Initialise SriLog Configuration Class Object
//Log.Logger = new LoggerConfiguration()
//             .ReadFrom.Configuration(configuration)
//             .Enrich.FromLogContext()
//             .Enrich.WithMachineName()
//             .Enrich.WithEnvironmentName()
//             .CreateLogger();
//Step 04:End

//Step 05:Start: Give Instruction to use Serilog for Logging
builder.Host.UseSerilog();
//Step 05:End

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Step 06:Start: Add Try and catch block and add logging in try and catch block
try
{
    
    Log.Information("Starting Web Host");
    Log.Information("Environment : {Env}", app.Environment.EnvironmentName);
    Log.Information("Machine :{MachineName}", Environment.MachineName);
    

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthorization();

    app.MapStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();


    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host Terminated Unexpecptedly");

}
finally
{
    Log.CloseAndFlush();
}

