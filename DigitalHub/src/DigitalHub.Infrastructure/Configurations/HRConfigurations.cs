using DigitalHub.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHub.Infrastructure.Configurations;


internal sealed class DepartmentMasterConfigurations : IEntityTypeConfiguration<DepartmentMaster>
{
    public void Configure(EntityTypeBuilder<DepartmentMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<DepartmentMaster>()
        {
            new DepartmentMaster { Id = 1, Name = "Sales" },
            new DepartmentMaster { Id = 2, Name = "Leasing" },
            new DepartmentMaster { Id = 3, Name = "Property Management" },
            new DepartmentMaster { Id = 4, Name = "Facilities Management" },
            new DepartmentMaster { Id = 5, Name = "Construction & Projects" },
            new DepartmentMaster { Id = 6, Name = "Legal & Compliance" },
            new DepartmentMaster { Id = 7, Name = "Customer Relations" },
            new DepartmentMaster { Id = 8, Name = "Finance & Accounting" },
            new DepartmentMaster { Id = 9, Name = "Marketing & Branding" },
            new DepartmentMaster { Id = 10, Name = "IT & Systems" },
            new DepartmentMaster { Id = 11, Name = "Human Resources" },
            new DepartmentMaster { Id = 12, Name = "Procurement & Contracts" },
            new DepartmentMaster { Id = 13, Name = "Business Development" },
            new DepartmentMaster { Id = 14, Name = "Strategic Planning" },
            new DepartmentMaster { Id = 15, Name = "Administration & Support" },

            // Hospitality-related
            new DepartmentMaster { Id = 16, Name = "Housekeeping" },
            new DepartmentMaster { Id = 17, Name = "Room Service" },
            new DepartmentMaster { Id = 18, Name = "Front Desk / Reception" },
            new DepartmentMaster { Id = 19, Name = "Security" },
            new DepartmentMaster { Id = 20, Name = "Food & Beverage" },
            new DepartmentMaster { Id = 21, Name = "Guest Services" },
            new DepartmentMaster { Id = 22, Name = "Concierge" },
            new DepartmentMaster { Id = 23, Name = "Laundry Services" },
            new DepartmentMaster { Id = 24, Name = "Maintenance & Engineering" }
        };

        entity.HasData(items);
    }
}

internal sealed class EmployeeRoleMasterConfigurations : IEntityTypeConfiguration<EmployeeRoleMaster>
{
    public void Configure(EntityTypeBuilder<EmployeeRoleMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<EmployeeRoleMaster>()
        {
            new EmployeeRoleMaster { Id = 1, Name = "Receptionist" },
            new EmployeeRoleMaster { Id = 2, Name = "Housekeeping Supervisor" },
            new EmployeeRoleMaster { Id = 3, Name = "Maintenance Technician" },
            new EmployeeRoleMaster { Id = 4, Name = "Security Officer" },
            new EmployeeRoleMaster { Id = 5, Name = "Room Service Attendant" },
            new EmployeeRoleMaster { Id = 6, Name = "Sales Manager" },
            new EmployeeRoleMaster { Id = 7, Name = "Leasing Consultant" },
            new EmployeeRoleMaster { Id = 8, Name = "Property Manager" },
            new EmployeeRoleMaster { Id = 9, Name = "Cleaner" },
            new EmployeeRoleMaster { Id = 10, Name = "IT Support" }
        };

        entity.HasData(items);
    }
}

internal sealed class EmploymentTypeMasterConfigurations : IEntityTypeConfiguration<EmploymentTypeMaster>
{
    public void Configure(EntityTypeBuilder<EmploymentTypeMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        // Seed initial data without description
        var items = new List<EmploymentTypeMaster>()
        {
            new EmploymentTypeMaster { Id = 1, Name = "Full-Time" },
            new EmploymentTypeMaster { Id = 2, Name = "Part-Time" },
            new EmploymentTypeMaster { Id = 3, Name = "Contractor" },
            new EmploymentTypeMaster { Id = 4, Name = "Intern" },
            new EmploymentTypeMaster { Id = 5, Name = "Temporary" }
        };

        entity.HasData(items);
    }
}

internal sealed class EmployeeMasterConfigurations : IEntityTypeConfiguration<EmployeeMaster>
{
    public void Configure(EntityTypeBuilder<EmployeeMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Title);
        entity.HasIndex(x => x.FirstName);
        entity.HasIndex(x => x.LastName);
        entity.HasIndex(x => x.Email);
        entity.HasIndex(x => x.PhoneNumber);
        entity.HasIndex(x => x.HireDate);

        var items = new List<EmployeeMaster>()
        {
            new EmployeeMaster { Id = 1, EmployeeCode = "EC-00001", DepartmentId = 1, RoleId = 1, EmploymentTypeId = 1, Title = "Mr.", FirstName = "Cyber", LastName = "Kemet", DateOfBirth = new DateTime(2022, 1, 1), Salary = 11000M, Currency = "USD", Gender = "Male", MaritalStatus = "Single", PhoneNumber = "+201210165412", Email = "ceo@digitalhub.com", HireDate = new DateTime(2022, 1,1) },
            new EmployeeMaster { Id = 2, EmployeeCode = "EC-00002", DepartmentId = 1, RoleId = 2, EmploymentTypeId = 1, Title = "Mrs.", FirstName = "Nora", LastName = "Mitchell", DateOfBirth = new DateTime(1990, 11, 5), Salary = 15000M, Currency = "USD", Gender = "Female", MaritalStatus = "Married", PhoneNumber = "+971505223344", Email = "nora.mitchell@example.com", HireDate = new DateTime(2018, 8,1) },
            new EmployeeMaster { Id = 3, EmployeeCode = "EC-00003", DepartmentId = 2, RoleId = 3, EmploymentTypeId = 2, Title = "Mr.", FirstName = "Ahmad", LastName = "Zaki", DateOfBirth = new DateTime(1982, 4, 18), Salary = 13000M, Currency = "USD", Gender = "Male", MaritalStatus = "Married", PhoneNumber = "+971506334455", Email = "ahmad.zaki@example.com", HireDate = new DateTime(2015, 2,1) },
            new EmployeeMaster { Id = 4, EmployeeCode = "EC-00004", DepartmentId = 3, RoleId = 4, EmploymentTypeId = 2, Title = "Ms.", FirstName = "Fatima", LastName = "Youssef", DateOfBirth = new DateTime(1995, 9, 12), Salary = 16000M, Currency = "USD", Gender = "Female", MaritalStatus = "Single", PhoneNumber = "+971507445566", Email = "fatima.youssef@example.com", HireDate = new DateTime(2022, 6,1) },
            new EmployeeMaster { Id = 5, EmployeeCode = "EC-00005", DepartmentId = 3, RoleId = 4, EmploymentTypeId = 3, Title = "Dr.", FirstName = "Victor", LastName = "Hanna", DateOfBirth = new DateTime(1978, 3, 22), Salary = 19000M, Currency = "USD", Gender = "Male", MaritalStatus = "Married", PhoneNumber = "+971508556677", Email = "victor.hanna@example.com", HireDate = new DateTime(2024, 11,1) },
            new EmployeeMaster { Id = 6, EmployeeCode = "EC-00006", DepartmentId = 4, RoleId = 5, EmploymentTypeId = 3, Title = "Mrs.", FirstName = "Mona", LastName = "Gerges", DateOfBirth = new DateTime(1988, 6, 10), Salary = 15000M, Currency = "USD", Gender = "Female", MaritalStatus = "Married", PhoneNumber = "+971509667788", Email = "mona.gerges@example.com", HireDate = new DateTime(2025, 7,1) },
            new EmployeeMaster { Id = 7, EmployeeCode = "EC-00007", DepartmentId = 5, RoleId = 5, EmploymentTypeId = 4, Title = "Mr.", FirstName = "Khaled", LastName = "Salem", DateOfBirth = new DateTime(1980, 2, 5), Salary = 16000M, Currency = "USD", Gender = "Male", MaritalStatus = "Single", PhoneNumber = "+971501778899", Email = "khaled.salem@example.com", HireDate = new DateTime(2011, 10,1) },
            new EmployeeMaster { Id = 8, EmployeeCode = "EC-00008", DepartmentId = 6, RoleId = 6, EmploymentTypeId = 4, Title = "Ms.", FirstName = "Rania", LastName = "Hassan", DateOfBirth = new DateTime(1992, 12, 3), Salary = 12000M, Currency = "USD", Gender = "Female", MaritalStatus = "Single", PhoneNumber = "+971502889900", Email = "rania.hassan@example.com", HireDate = new DateTime(2016, 2,1) }
        };

        entity.HasData(items);

    }
}
