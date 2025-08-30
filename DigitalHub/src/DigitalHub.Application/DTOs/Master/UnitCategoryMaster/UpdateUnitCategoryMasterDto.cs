namespace DigitalHub.Application.DTOs.Master.UnitCategoryMaster;

public record UpdateUnitCategoryMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
