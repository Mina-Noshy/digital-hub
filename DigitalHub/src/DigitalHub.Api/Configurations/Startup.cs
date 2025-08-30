namespace DigitalHub.Api.Configurations;

public static class Startup
{
    public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        const string configurationsDirectory = "Configurations";
        var env = builder.Environment;

        builder.Configuration
            // Configurations
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            .AddJsonFile($"{configurationsDirectory}/database.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/database.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            .AddJsonFile($"{configurationsDirectory}/logger.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/logger.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            .AddJsonFile($"{configurationsDirectory}/ratelimiter.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/ratelimiter.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            .AddJsonFile($"{configurationsDirectory}/jwt.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/jwt.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            .AddJsonFile($"{configurationsDirectory}/cors.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/cors.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            .AddJsonFile($"{configurationsDirectory}/url.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/url.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            .AddJsonFile($"{configurationsDirectory}/securityheader.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/securityheader.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            .AddEnvironmentVariables();

        return builder;
    }

    public static WebApplicationBuilder AddLocalization(this WebApplicationBuilder builder)
    {
        const string configurationsDirectory = "Configurations";
        var env = builder.Environment;

        builder.Configuration
            // Messages
            .AddJsonFile($"{configurationsDirectory}/Localization/Messages/msg-ar.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/Localization/Messages/msg-ar.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            .AddJsonFile($"{configurationsDirectory}/Localization/Messages/msg-en.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/Localization/Messages/msg-en.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)


            // Emails
            .AddJsonFile($"{configurationsDirectory}/Localization/Emails/email-ar.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/Localization/Emails/email-ar.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            .AddJsonFile($"{configurationsDirectory}/Localization/Emails/email-en.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/Localization/Emails/email-en.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            .AddEnvironmentVariables();

        return builder;
    }
}
