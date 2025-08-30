namespace DigitalHub.Application.DTOs.HR.EmploymentTypeMaster;

public record CreateEmploymentTypeMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
