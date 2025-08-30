using DigitalHub.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Infrastructure.Persistence.Context;

public partial class MainDbContext
{

    #region HR Entities
    public DbSet<DepartmentMaster> DepartmentMaster { get; set; }
    public DbSet<EmployeeRoleMaster> EmployeeRoleMaster { get; set; }
    public DbSet<EmploymentTypeMaster> EmploymentTypeMaster { get; set; }
    public DbSet<EmployeeMaster> EmployeeMaster { get; set; }
    #endregion
}
