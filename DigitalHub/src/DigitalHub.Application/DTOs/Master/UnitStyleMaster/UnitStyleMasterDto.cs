namespace DigitalHub.Application.DTOs.Master.UnitStyleMaster;

public record UnitStyleMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
