namespace DigitalHub.Application.DTOs.Master.UnitModelMaster;

public record UnitModelMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
