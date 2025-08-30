using DigitalHub.Domain.Interfaces.Common;
using DigitalHub.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalHub.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainLayerExtensions(this IServiceCollection services)
        {
            services
                .AddServices();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IPathFinderService, PathFinderService>();
            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
