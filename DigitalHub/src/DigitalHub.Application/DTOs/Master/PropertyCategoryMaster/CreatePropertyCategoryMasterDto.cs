namespace DigitalHub.Application.DTOs.Master.PropertyCategoryMaster;

public record CreatePropertyCategoryMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
