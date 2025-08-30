using DigitalHub.Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Infrastructure.Persistence.Context;

public partial class MainDbContext
{

    #region Master Entities
    public DbSet<CountryMaster> CountryMaster { get; set; }
    public DbSet<CityMaster> CityMaster { get; set; }
    public DbSet<BankMaster> BankMaster { get; set; }
    public DbSet<GenderMaster> GenderMaster { get; set; }
    public DbSet<MaritalStatusMaster> MaritalStatusMaster { get; set; }
    public DbSet<NationalityMaster> NationalityMaster { get; set; }
    public DbSet<UnitTypeMaster> UnitTypeMaster { get; set; }
    public DbSet<UnitViewMaster> UnitViewMaster { get; set; }
    public DbSet<UnitCategoryMaster> UnitCategoryMaster { get; set; }
    public DbSet<UnitStyleMaster> UnitStyleMaster { get; set; }
    public DbSet<UnitModelMaster> UnitModelMaster { get; set; }
    public DbSet<UnitClassMaster> UnitClassMaster { get; set; }
    public DbSet<PropertyCategoryMaster> PropertyCategoryMaster { get; set; }
    public DbSet<PropertyTypeMaster> PropertyTypeMaster { get; set; }
    public DbSet<TitleMaster> TitleMaster { get; set; }
    public DbSet<TermsAndConditionsCategoryMaster> TermsAndConditionsCategoryMaster { get; set; }
    public DbSet<TermsAndConditionsMaster> TermsAndConditionsMaster { get; set; }
    public DbSet<CommunicationMethodMaster> CommunicationMethodMaster { get; set; }
    public DbSet<PriorityMaster> PriorityMaster { get; set; }
    public DbSet<FloorMaster> FloorMaster { get; set; }
    public DbSet<FrequencyMaster> FrequencyMaster { get; set; }
    #endregion

}
