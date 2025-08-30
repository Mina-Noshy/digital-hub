namespace DigitalHub.Application.DTOs.Master.UnitViewMaster;

public record CreateUnitViewMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
