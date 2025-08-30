namespace DigitalHub.Application.DTOs.Master.PropertyCategoryMaster;

public record PropertyCategoryMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
