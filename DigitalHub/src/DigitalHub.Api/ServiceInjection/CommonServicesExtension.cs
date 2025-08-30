namespace DigitalHub.Api.ServiceInjection;

public static class CommonServicesExtension
{
    public static IServiceCollection AddCommonServicesExtension(this IServiceCollection services)
    {
        services
            .AddMemoryCache()
            .AddHttpContextAccessor()
            .AddControllers()
            .AddNewtonsoftJson();

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });

        return services;
    }
}
