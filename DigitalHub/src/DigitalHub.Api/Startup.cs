using Asp.Versioning.ApiExplorer;
using DigitalHub.Api.Configurations;
using DigitalHub.Api.Loggers;
using DigitalHub.Api.Middlewares;
using DigitalHub.Api.Security;
using DigitalHub.Api.ServiceInjection;
using DigitalHub.Domain;
using DigitalHub.Domain.Interfaces.Common;
using DigitalHub.Domain.Utilities;
using DigitalHub.Infrastructure;
using Serilog;

namespace DigitalHub.Api;

public static class StartupExtensions
{
    public static void ConfigureHostSettings(this ConfigureHostBuilder host, IConfiguration configuration, IWebHostEnvironment environment, WebApplicationBuilder builder)
    {
        host.UseSerilog();
        builder.AddConfigurations();
        builder.AddLocalization();

        ConfigurationHelper.Initialize(configuration, environment);
        SerilogConfig.Initialize();
    }

    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services
            .AddInfrastructureLayerExtensions()
            .AddDomainLayerExtensions()
            .AddApiLayerExtensions();
    }

    public static void ConfigureApplication(this WebApplication app)
    {
        app.RegisterApplicationStarted();

        // Create a new scope and inject ICurrentUser
        using (var scope = app.Services.CreateScope())
        {
            var currentUser = scope.ServiceProvider.GetRequiredService<ICurrentUser>();
            ConfigurationHelper.Initialize(currentUser);
        }

        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseApplicationPipeline(app.Environment, provider, app.Configuration);
    }
}

public static class MiddlewareExtensions
{
    public static IServiceCollection AddApiLayerExtensions(this IServiceCollection services)
    {
        return services
            .AddApiVersioningExtension()
            .AddRateLimiterExtension()
            .AddCommonServicesExtension()
            .AddMiddlewareExtension()
            .AddCorsExtension()
            .AddMediatRExtension()
            .AddExceptionHandlerExtension()
            .AddSwaggerExtension()
            .AddJwtAuthenticationExtension();
    }

    public static IApplicationBuilder UseApplicationPipeline(
        this IApplicationBuilder app,
        IWebHostEnvironment env,
        IApiVersionDescriptionProvider provider,
        IConfiguration configuration)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger()
               .UseSwaggerUI(o =>
               {
                   foreach (var description in provider.ApiVersionDescriptions)
                   {
                       o.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                   }
                   o.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
               });
        }
        else
        {
            app.UseSwagger()
               .UseSwaggerUI(o =>
               {
                   foreach (var description in provider.ApiVersionDescriptions)
                   {
                       o.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                   }
                   o.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
               });
        }

        return app
            .UseMiddleware<IpBlockingMiddleware>()
            .UseRateLimiter()
            .UseStaticFiles()
            .UseExceptionHandler()
            .UseSerilogRequestLogging()
            .UseCors("DefaultCorePolicy")
            .UseHttpsRedirection()
            .UseRouting()
            .UseSecurityHeaders(configuration)
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
