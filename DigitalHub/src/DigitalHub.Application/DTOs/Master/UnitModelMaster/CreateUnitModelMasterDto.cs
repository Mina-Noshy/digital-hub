namespace DigitalHub.Application.DTOs.Master.UnitModelMaster;

public record CreateUnitModelMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
