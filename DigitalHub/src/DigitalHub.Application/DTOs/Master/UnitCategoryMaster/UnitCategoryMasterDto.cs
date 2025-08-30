namespace DigitalHub.Application.DTOs.Master.UnitCategoryMaster;

public record UnitCategoryMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
