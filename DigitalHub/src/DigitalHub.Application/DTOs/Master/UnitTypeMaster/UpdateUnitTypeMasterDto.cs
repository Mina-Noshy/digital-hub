namespace DigitalHub.Application.DTOs.Master.UnitTypeMaster;

public record UpdateUnitTypeMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
