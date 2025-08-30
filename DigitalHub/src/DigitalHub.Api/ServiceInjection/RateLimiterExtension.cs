using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.CompanyProfileSettings;
using DigitalHub.Domain.Constants;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using DigitalHub.Domain.Utilities;
using Mapster;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace DigitalHub.Api.ServiceInjection;

public static class RateLimiterExtension
{

    public static IServiceCollection AddRateLimiterExtension(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = int.Parse(ConfigurationHelper.GetRateLimiter(RateLimiterKeys.Requests)),
                        QueueLimit = 0,
                        Window = TimeSpan.FromSeconds(int.Parse(ConfigurationHelper.GetRateLimiter(RateLimiterKeys.Duration)))
                    }));

            options.OnRejected = async (context, token) =>
            {
                var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    // Store blocked IPs (using MemoryCache for persistence)
                    var cache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
                    var cacheKey = $"BlockedIP_{ipAddress}";
                    var duration = int.Parse(ConfigurationHelper.GetRateLimiter(RateLimiterKeys.IpBlockDuration));

                    // Block for 5 minutes
                    cache.Set(cacheKey, true, TimeSpan.FromMinutes(duration));
                }

                // Send security alert email.
                await ConfigureSecurityAlert(context.HttpContext);

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                var retryAfterSeconds = context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter) ? retryAfter.TotalSeconds : (double?)null;
                var msg = $"Too many requests. Please try again after {retryAfter.TotalSeconds} seconds(s).";

                var response = ApiResponse.RateLimited(msg);

                var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

                await context.HttpContext.Response.WriteAsync(jsonResponse, cancellationToken: token);

            };
        });

        return services;
    }

    private async static Task ConfigureSecurityAlert(HttpContext httpContext)
    {
        var settingsRepository = httpContext.RequestServices.GetRequiredService<ICompanyProfileSettingsRepository>();
        var emailSender = httpContext.RequestServices.GetRequiredService<IEmailSender>();
        var settings = await settingsRepository.GetSettingsAsync(CancellationToken.None);
        var settingsDto = settings.Adapt<CompanyProfileSettingsDto>();

        string attackerInfo = @$"
                An attacker is trying to access the system. <br />  
                Attacker Info: <br /> <br /> 

                <strong>IP:</strong> {httpContext.Connection.RemoteIpAddress?.ToString()}, <br />
                <strong>UserAgent:</strong> {httpContext.Request.Headers["User - Agent"].ToString()}, <br />
                <strong>RequestPath:</strong> {httpContext.Request.Path}, <br />
                <strong>Host:</strong> {httpContext.Request.Headers.Host.ToString()}, <br />
                <strong>Timestamp:</strong> {DateTimeProvider.UtcNow}
                ";

        await SendSecurityEmail(emailSender, attackerInfo, settingsDto);
    }

    private async static Task SendSecurityEmail(IEmailSender emailSender, string attackerInfo, CompanyProfileSettingsDto settings)
    {
        string securityEmail = settings!.SupportEmail!;

        var isSent = await emailSender
            .Compose()
            .To(securityEmail)
            .Subject("Security Alert")
            .Body(attackerInfo, true)
            .SendAsync(CancellationToken.None);
    }
}
