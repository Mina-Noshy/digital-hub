namespace DigitalHub.Application.DTOs.Master.FloorMaster;

public record FloorMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
