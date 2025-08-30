using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Serilog;

namespace DigitalHub.Api.ServiceInjection;

public static class ApplicationStartedExtension
{
    public static void RegisterApplicationStarted(this WebApplication app)
    {
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            var server = app.Services.GetService<IServer>();
            var addresses = server?.Features.Get<IServerAddressesFeature>()?.Addresses;

            if (addresses != null)
            {
                foreach (var address in addresses)
                {
                    Log.Warning($"Digital Hub :: {address}");
                }
            }
        });
    }
}
