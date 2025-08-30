namespace DigitalHub.Application.DTOs.Master.TitleMaster;

public record TitleMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
