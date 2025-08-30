namespace DigitalHub.Application.DTOs.Master.UnitClassMaster;

public record CreateUnitClassMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
