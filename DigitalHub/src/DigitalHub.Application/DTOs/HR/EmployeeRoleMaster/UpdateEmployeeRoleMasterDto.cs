namespace DigitalHub.Application.DTOs.HR.EmployeeRoleMaster;

public record UpdateEmployeeRoleMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
