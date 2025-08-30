namespace DigitalHub.Application.DTOs.Master.PropertyTypeMaster;

public record CreatePropertyTypeMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
