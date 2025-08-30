namespace DigitalHub.Application.DTOs.Master.MaritalStatusMaster;

public record CreateMaritalStatusMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
