namespace DigitalHub.Api.Security;

internal static class SecurityHeadersExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, IConfiguration config)
    {
        var settings = config.GetSection(nameof(SecurityHeaderSettings)).Get<SecurityHeaderSettings>();

        if (settings?.Enable == true)
        {
            app.Use(async (context, next) =>
            {
                // Check and apply headers if not null or whitespace
                AddHeaderIfNotNullOrEmpty(context, HeaderNames.XFRAMEOPTIONS, settings.XFrameOptions);
                AddHeaderIfNotNullOrEmpty(context, HeaderNames.XCONTENTTYPEOPTIONS, settings.XContentTypeOptions);
                AddHeaderIfNotNullOrEmpty(context, HeaderNames.REFERRERPOLICY, settings.ReferrerPolicy);
                AddHeaderIfNotNullOrEmpty(context, HeaderNames.PERMISSIONSPOLICY, settings.PermissionsPolicy);
                AddHeaderIfNotNullOrEmpty(context, HeaderNames.SAMESITE, settings.SameSite);
                AddHeaderIfNotNullOrEmpty(context, HeaderNames.XXSSPROTECTION, settings.XXSSProtection);


                await next();
            });
        }

        return app;
    }

    // Helper method to add header safely, avoiding duplicates
    private static void AddHeaderIfNotNullOrEmpty(HttpContext context, string headerName, string headerValue)
    {
        if (!string.IsNullOrWhiteSpace(headerValue))
        {
            // Use Append to ensure we don't overwrite existing headers, allowing additional values to be added
            context.Response.Headers.Append(headerName, headerValue);
        }
    }
}
