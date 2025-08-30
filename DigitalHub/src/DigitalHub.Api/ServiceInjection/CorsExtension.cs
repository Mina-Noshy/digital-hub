using DigitalHub.Domain.Constants;
using DigitalHub.Domain.Utilities;

namespace DigitalHub.Api.ServiceInjection;

public static class CorsExtension
{
    public static IServiceCollection AddCorsExtension(this IServiceCollection services)
    {
        var domains = ConfigurationHelper.GetCORS(CORSKeys.Domains).Split(",");

        services.AddCors(options =>
        {
            options.AddPolicy("DefaultCorePolicy",
                policy =>
                {
                    policy.WithOrigins(domains) // Allow multiple domains
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });

        return services;
    }
}
