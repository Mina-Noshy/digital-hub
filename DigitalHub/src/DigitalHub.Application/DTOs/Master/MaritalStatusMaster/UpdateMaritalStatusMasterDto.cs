namespace DigitalHub.Application.DTOs.Master.MaritalStatusMaster;

public record UpdateMaritalStatusMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
