using DigitalHub.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Infrastructure.Persistence.Context;

public partial class MainDbContext
{

    #region Auth Entities
    public DbSet<ModuleMaster> ModuleMaster { get; set; }
    public DbSet<MenuMaster> MenuMaster { get; set; }
    public DbSet<PageMaster> PageMaster { get; set; }
    public DbSet<RolePageMaster> RolePageMaster { get; set; }
    public DbSet<CompanyDatabaseMaster> CompanyDatabaseMaster { get; set; }
    public DbSet<UserLoginHistoryMaster> UserLoginHistoryMaster { get; set; }
    public DbSet<EntityChangeLog> EntityChangeLogs { get; set; }
    public DbSet<NotificationMaster> NotificationMaster { get; set; }
    public DbSet<CompanyProfileSettings> CompanyProfileSettings { get; set; }
    #endregion

}
