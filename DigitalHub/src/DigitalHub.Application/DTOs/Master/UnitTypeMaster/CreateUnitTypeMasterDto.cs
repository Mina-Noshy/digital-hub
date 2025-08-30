namespace DigitalHub.Application.DTOs.Master.UnitTypeMaster;

public record CreateUnitTypeMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
