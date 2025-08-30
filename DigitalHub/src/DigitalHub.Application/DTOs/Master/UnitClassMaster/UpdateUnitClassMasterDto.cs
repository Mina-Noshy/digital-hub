namespace DigitalHub.Application.DTOs.Master.UnitClassMaster;

public record UpdateUnitClassMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
