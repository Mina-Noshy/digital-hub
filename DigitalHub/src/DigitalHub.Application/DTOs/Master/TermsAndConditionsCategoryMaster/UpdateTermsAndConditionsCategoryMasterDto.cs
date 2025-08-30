namespace DigitalHub.Application.DTOs.Master.TermsAndConditionsCategoryMaster;

public record UpdateTermsAndConditionsCategoryMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
