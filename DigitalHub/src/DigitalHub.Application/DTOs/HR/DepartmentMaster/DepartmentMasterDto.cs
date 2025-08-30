namespace DigitalHub.Application.DTOs.HR.DepartmentMaster;

public record DepartmentMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
