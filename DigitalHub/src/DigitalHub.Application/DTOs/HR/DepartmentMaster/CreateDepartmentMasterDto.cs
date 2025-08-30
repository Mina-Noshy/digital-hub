namespace DigitalHub.Application.DTOs.HR.DepartmentMaster;

public record CreateDepartmentMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
