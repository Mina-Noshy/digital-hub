namespace DigitalHub.Application.DTOs.HR.EmployeeRoleMaster;

public record EmployeeRoleMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
