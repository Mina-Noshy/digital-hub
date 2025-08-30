using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Infrastructure.Repositories.Master;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalHub.Infrastructure.Repositories.Common;

public static class MasterRepositoryInjection
{
    public static IServiceCollection AddMasterRepositories(this IServiceCollection services)
    {
        #region Master Repositories
        services.AddScoped<IBankMasterRepository, BankMasterRepository>();
        services.AddScoped<ICountryMasterRepository, CountryMasterRepository>();
        services.AddScoped<ICityMasterRepository, CityMasterRepository>();
        services.AddScoped<IGenderMasterRepository, GenderMasterRepository>();
        services.AddScoped<IMaritalStatusMasterRepository, MaritalStatusMasterRepository>();
        services.AddScoped<INationalityMasterRepository, NationalityMasterRepository>();
        services.AddScoped<ITermsAndConditionsCategoryMasterRepository, TermsAndConditionsCategoryMasterRepository>();
        services.AddScoped<ITermsAndConditionsMasterRepository, TermsAndConditionsMasterRepository>();
        services.AddScoped<IUnitCategoryMasterRepository, UnitCategoryMasterRepository>();
        services.AddScoped<IUnitClassMasterRepository, UnitClassMasterRepository>();
        services.AddScoped<IUnitModelMasterRepository, UnitModelMasterRepository>();
        services.AddScoped<IUnitStyleMasterRepository, UnitStyleMasterRepository>();
        services.AddScoped<IUnitViewMasterRepository, UnitViewMasterRepository>();
        services.AddScoped<ITitleMasterRepository, TitleMasterRepository>();
        services.AddScoped<IUnitTypeMasterRepository, UnitTypeMasterRepository>();
        services.AddScoped<ICommunicationMethodMasterRepository, CommunicationMethodMasterRepository>();
        services.AddScoped<IPriorityMasterRepository, PriorityMasterRepository>();
        services.AddScoped<IFloorMasterRepository, FloorMasterRepository>();
        services.AddScoped<IPropertyCategoryMasterRepository, PropertyCategoryMasterRepository>();
        services.AddScoped<IPropertyTypeMasterRepository, PropertyTypeMasterRepository>();
        services.AddScoped<IFrequencyMasterRepository, FrequencyMasterRepository>();
        #endregion

        return services;
    }
}
