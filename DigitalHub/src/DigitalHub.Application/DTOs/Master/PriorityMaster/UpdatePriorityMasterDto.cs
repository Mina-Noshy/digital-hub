namespace DigitalHub.Application.DTOs.Master.PriorityMaster;

public record UpdatePriorityMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
