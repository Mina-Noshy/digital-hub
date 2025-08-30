using DigitalHub.Application.Common;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace DigitalHub.Api.Middlewares;

public class IpBlockingMiddleware(RequestDelegate _next, IMemoryCache _cache)
{

    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString();

        if (!string.IsNullOrEmpty(ip))
        {
            var cacheKey = $"BlockedIP_{ip}";

            // Check if IP is blocked
            if (_cache.TryGetValue(cacheKey, out bool isBlocked) && isBlocked)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                var msg = "Access Denied! We have temporarily blocked your IP for [?] minutes. Either you are too enthusiastic... or trying to be sneaky. Take a break, touch some grass, and try again later.";

                var response = ApiResponse.RateLimited(msg);

                var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

                await context.Response.WriteAsync(jsonResponse, CancellationToken.None);
                return;
            }
        }

        await _next(context);
    }
}
