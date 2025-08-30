using DigitalHub.Domain.Constants;
using DigitalHub.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace DigitalHub.Infrastructure.Persistence;

public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
{
    public MainDbContext CreateDbContext(string[] args)
    {
        // 1. Build configuration manually
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configurations"))
            .AddJsonFile("database.json", optional: false)
            .AddJsonFile("database.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        // 2. Read the connection string
        var connectionString = configuration.GetConnectionString(ConnectionStringKeys.Development);

        var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        // Suppress the warning about pending model changes to prevent EF Core from throwing exceptions.
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        return new MainDbContext(optionsBuilder.Options);
    }
}
