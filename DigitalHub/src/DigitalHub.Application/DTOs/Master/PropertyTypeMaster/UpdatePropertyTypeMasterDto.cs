namespace DigitalHub.Application.DTOs.Master.PropertyTypeMaster;

public record UpdatePropertyTypeMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
