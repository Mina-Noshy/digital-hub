using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Infrastructure.Repositories.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalHub.Infrastructure.Repositories.Common;

public static class AuthRepositoryInjection
{
    public static IServiceCollection AddAuthRepositories(this IServiceCollection services)
    {
        #region Auth Repositories
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IRoleMasterRepository, RoleMasterRepository>();
        services.AddScoped<IModuleMasterRepository, ModuleMasterRepository>();
        services.AddScoped<IMenuMasterRepository, MenuMasterRepository>();
        services.AddScoped<IPageMasterRepository, PageMasterRepository>();
        services.AddScoped<IRolePageMasterRepository, RolePageMasterRepository>();
        services.AddScoped<ICompanyDatabaseMasterRepository, CompanyDatabaseMasterRepository>();
        services.AddScoped<IUserLoginHistoryMasterRepository, UserLoginHistoryMasterRepository>();
        services.AddScoped<IEntityChangeLogRepository, EntityChangeLogRepository>();
        services.AddScoped<INotificationMasterRepository, NotificationMasterRepository>();
        services.AddScoped<ICompanyProfileSettingsRepository, CompanyProfileSettingsRepository>();
        #endregion

        return services;
    }
}
