namespace DigitalHub.Application.DTOs.Master.FloorMaster;

public record CreateFloorMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
