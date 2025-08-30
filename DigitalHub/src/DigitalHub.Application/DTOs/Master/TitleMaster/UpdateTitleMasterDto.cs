namespace DigitalHub.Application.DTOs.Master.TitleMaster;

public record UpdateTitleMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
