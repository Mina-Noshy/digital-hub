namespace DigitalHub.Application.DTOs.Master.UnitStyleMaster;

public record CreateUnitStyleMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
