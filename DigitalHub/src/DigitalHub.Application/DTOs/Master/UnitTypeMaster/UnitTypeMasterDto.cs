namespace DigitalHub.Application.DTOs.Master.UnitTypeMaster;

public record UnitTypeMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
