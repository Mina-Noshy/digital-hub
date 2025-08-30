namespace DigitalHub.Application.DTOs.Master.TitleMaster;

public record CreateTitleMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
