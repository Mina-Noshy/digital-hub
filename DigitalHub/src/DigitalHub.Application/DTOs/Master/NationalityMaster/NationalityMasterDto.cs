namespace DigitalHub.Application.DTOs.Master.NationalityMaster;

public record NationalityMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
