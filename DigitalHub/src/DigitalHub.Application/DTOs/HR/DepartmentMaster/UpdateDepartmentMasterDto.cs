namespace DigitalHub.Application.DTOs.HR.DepartmentMaster;

public record UpdateDepartmentMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
