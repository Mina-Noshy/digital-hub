namespace DigitalHub.Application.DTOs.HR.EmploymentTypeMaster;

public record UpdateEmploymentTypeMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
