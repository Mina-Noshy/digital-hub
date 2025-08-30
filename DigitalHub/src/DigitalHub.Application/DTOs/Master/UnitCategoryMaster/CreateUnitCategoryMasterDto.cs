namespace DigitalHub.Application.DTOs.Master.UnitCategoryMaster;

public record CreateUnitCategoryMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
