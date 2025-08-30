namespace DigitalHub.Application.DTOs.Master.NationalityMaster;

public record UpdateNationalityMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
