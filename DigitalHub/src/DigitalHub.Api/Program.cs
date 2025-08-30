using DigitalHub.Api;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Configure host settings
    builder.Host.ConfigureHostSettings(builder.Configuration, builder.Environment, builder);

    Log.Warning("Digital Hub :: Web host booting up");

    // Register application services
    builder.Services.RegisterApplicationServices();

    // Build and configure the application
    var app = builder.Build();
    app.ConfigureApplication();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Digital Hub :: Unhandled exception, API server startup failed!");
}
finally
{
    Log.Information("Digital Hub :: API server shutting down...");
    Log.CloseAndFlush();
}
