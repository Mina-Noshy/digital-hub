using DigitalHub.Domain.Interfaces.HR;
using DigitalHub.Infrastructure.Repositories.HR;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalHub.Infrastructure.Repositories.Common;

public static class HrRepositoryInjection
{
    public static IServiceCollection AddHrRepositories(this IServiceCollection services)
    {
        #region HR Repositories
        services.AddScoped<IDepartmentMasterRepository, DepartmentMasterRepository>();
        services.AddScoped<IEmployeeRoleMasterRepository, EmployeeRoleMasterRepository>();
        services.AddScoped<IEmploymentTypeMasterRepository, EmploymentTypeMasterRepository>();
        services.AddScoped<IEmployeeMasterRepository, EmployeeMasterRepository>();
        #endregion

        return services;
    }
}
