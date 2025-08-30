namespace DigitalHub.Application.DTOs.Master.GenderMaster;

public record CreateGenderMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
