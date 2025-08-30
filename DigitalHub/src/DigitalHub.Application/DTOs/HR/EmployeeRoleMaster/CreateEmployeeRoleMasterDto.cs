namespace DigitalHub.Application.DTOs.HR.EmployeeRoleMaster;

public record CreateEmployeeRoleMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
