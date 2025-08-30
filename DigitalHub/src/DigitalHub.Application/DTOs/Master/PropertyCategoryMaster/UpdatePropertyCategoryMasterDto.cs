namespace DigitalHub.Application.DTOs.Master.PropertyCategoryMaster;

public record UpdatePropertyCategoryMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
