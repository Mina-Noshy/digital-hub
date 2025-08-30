namespace DigitalHub.Application.DTOs.Master.UnitClassMaster;

public record UnitClassMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
