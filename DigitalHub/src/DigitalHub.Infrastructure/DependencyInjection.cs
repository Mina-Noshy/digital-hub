using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Infrastructure.Persistence;
using DigitalHub.Infrastructure.Persistence.Context;
using DigitalHub.Infrastructure.Repositories.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DigitalHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayerExtensions(this IServiceCollection services)
    {
        services
            .AddContexts()
            .AddIdentity()
            .AddRepositories()
            .AddDatabaseStandard();

        return services;
    }

    // Alternative overload for backward compatibility
    public static IServiceCollection AddContexts(this IServiceCollection services)
    {
        // Get the environment from service provider if not provided
        var serviceProvider = services.BuildServiceProvider();
        var env = serviceProvider.GetRequiredService<IHostEnvironment>();

        return services.AddContexts(env);
    }

    public static IServiceCollection AddContexts(this IServiceCollection services, IHostEnvironment env)
    {
        services.AddScoped<EntityChangeInterceptor>();

        services.AddDbContext<MainDbContext>((serviceProvider, options) =>
        {
            var databaseRouter = serviceProvider.GetRequiredService<IDatabaseRouter>();
            var connection = databaseRouter.GetRoutedConnection();

            options.UseSqlServer(connection, sqlServerOptions =>
            {
                sqlServerOptions.MigrationsAssembly("DigitalHub.Api");
                sqlServerOptions.CommandTimeout(600);

                // Disable retry on failure for better resilience
                //sqlServerOptions.EnableRetryOnFailure(maxRetryCount: 0);
            });

            // Development-only configurations
            if (env.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }

            // Disable query tracking by default for better performance
            //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            // Add the interceptor
            options.AddInterceptors(serviceProvider.GetRequiredService<EntityChangeInterceptor>());
        }, ServiceLifetime.Scoped);

        // Register the abstraction
        services.AddScoped<IMainDbContext>(provider => provider.GetRequiredService<MainDbContext>());
        return services;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<UserMaster, RoleMaster>(
            options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // SignIn settings.
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<MainDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddDatabaseStandard(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        services.AddScoped<IDatabaseRouter, DatabaseRouter>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddAuthRepositories()
            .AddMasterRepositories()
            .AddHrRepositories();

        return services;
    }
}
