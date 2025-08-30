namespace DigitalHub.Application.DTOs.HR.EmploymentTypeMaster;

public record EmploymentTypeMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
