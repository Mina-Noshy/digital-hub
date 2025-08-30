namespace DigitalHub.Application.DTOs.Master.UnitViewMaster;

public record UpdateUnitViewMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
