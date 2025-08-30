using DigitalHub.Domain.Constants;
using DigitalHub.Domain.Utilities;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace DigitalHub.Api.Loggers;

public static class SerilogConfig
{
    public static void Initialize()
    {
        string filePath = ConfigurationHelper.GetSerilog(SerilogKeys.Path)
            .Replace("{date}", DateTimeProvider.UtcNow.ToString("yyyyMMddHHmmss"));

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Warning() // Set global minimum log level to Warning
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Suppress Microsoft internal logs below Warning
            .WriteTo.Console()
            .WriteTo.File(filePath, rollingInterval: RollingInterval.Day)
            .WriteTo.MSSqlServer(ConfigurationHelper.GetConnectionString(),
                        new MSSqlServerSinkOptions
                        {
                            TableName = ConfigurationHelper.GetSerilog(SerilogKeys.TableName),
                            SchemaName = ConfigurationHelper.GetSerilog(SerilogKeys.SchemaName),
                            AutoCreateSqlTable = bool.Parse(ConfigurationHelper.GetSerilog(SerilogKeys.AutoCreateSqlTable))
                        })
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .CreateLogger();
    }
}
