namespace DigitalHub.Application.DTOs.Master.UnitViewMaster;

public record UnitViewMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
