namespace DigitalHub.Application.DTOs.Master.PriorityMaster;

public record PriorityMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
