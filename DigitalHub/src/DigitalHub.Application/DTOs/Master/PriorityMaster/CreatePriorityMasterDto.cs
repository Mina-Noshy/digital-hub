namespace DigitalHub.Application.DTOs.Master.PriorityMaster;

public record CreatePriorityMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
