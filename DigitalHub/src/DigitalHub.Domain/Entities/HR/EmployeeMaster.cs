using DigitalHub.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalHub.Domain.Entities.HR;

public class EmployeeMaster : HumanDetails
{
    public long DepartmentId { get; set; }
    public long RoleId { get; set; }
    public long EmploymentTypeId { get; set; }
    public string EmployeeCode { get; set; } = null!;
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public string Currency { get; set; } = null!;


    [ForeignKey(nameof(DepartmentId))]
    public virtual DepartmentMaster GetDepartment { get; set; } = null!;

    [ForeignKey(nameof(RoleId))]
    public virtual EmployeeRoleMaster GetEmployeeRole { get; set; } = null!;

    [ForeignKey(nameof(EmploymentTypeId))]
    public virtual EmploymentTypeMaster GetEmploymentType { get; set; } = null!;

}
