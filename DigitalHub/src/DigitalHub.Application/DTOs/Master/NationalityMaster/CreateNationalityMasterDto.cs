namespace DigitalHub.Application.DTOs.Master.NationalityMaster;

public record CreateNationalityMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
