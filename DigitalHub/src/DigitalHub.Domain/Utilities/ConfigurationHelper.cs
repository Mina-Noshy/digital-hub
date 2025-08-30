using DigitalHub.Domain.Constants;
using DigitalHub.Domain.Interfaces.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DigitalHub.Domain.Utilities
{
    public class ConfigurationHelper
    {
        private const string _defaultLang = "en";
        private static IConfiguration? _configuration;
        private static IWebHostEnvironment? _environment;
        private static ICurrentUser? _currentUser;

        // Initialize the configuration and environment and set the default language
        public static void Initialize(IConfiguration configuration, IWebHostEnvironment environment)
            => (_configuration, _environment) = (configuration, environment);
        public static void Initialize(ICurrentUser? currentUser)
            => (_currentUser) = (currentUser);





        // Get Serilog configuration by key
        public static string GetSerilog(string key)
            => _configuration![$"Serilog:{key}"] ?? string.Empty;

        // Get Rate Limiter configuration by key
        public static string GetRateLimiter(string key)
            => _configuration![$"RateLimiter:{key}"] ?? string.Empty;

        // Get JWT configuration by key
        public static string GetJWT(string key)
            => _configuration![$"JWT:{key}"] ?? string.Empty;

        // Get CORS configuration by key
        public static string GetCORS(string key)
            => _configuration![$"CORS:{key}"] ?? string.Empty;

        // Get URL configuration by key
        public static string GetURL(string key)
            => _configuration![$"URLs:{key}"] ?? string.Empty;

        // Get connection string based on environment
        public static string GetConnectionString()
            => _environment!.IsDevelopment() ? GetDevelopmentConnectionString() : GetProductionConnectionString();

        // Get production connection string
        public static string GetProductionConnectionString()
            => _configuration![$"ConnectionStrings:{ConnectionStringKeys.Production}"] ?? string.Empty;

        // Get development connection string
        public static string GetDevelopmentConnectionString()
            => _configuration![$"ConnectionStrings:{ConnectionStringKeys.Development}"] ?? string.Empty;









        // Get localized message string for the given key
        public static string GetLocalizedMessageString(string key)
        {
            var lang = string.IsNullOrWhiteSpace(_currentUser?.Language.ToString()) ? _defaultLang : _currentUser.Language.ToString();
            var langKey = "Msg-" + lang.ToUpper();
            return _configuration![$"{langKey}:{key}"] ?? key;
        }

        // Get localized email string for the given key
        public static string GetLocalizedEmailString(string key)
        {
            var lang = string.IsNullOrWhiteSpace(_currentUser?.Language.ToString()) ? _defaultLang : _currentUser.Language.ToString();
            var langKey = "Email-" + lang.ToUpper();
            return _configuration![$"{langKey}:{key}"] ?? key;
        }


    }
}
