namespace DigitalHub.Application.DTOs.Master.GenderMaster;

public record GenderMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
