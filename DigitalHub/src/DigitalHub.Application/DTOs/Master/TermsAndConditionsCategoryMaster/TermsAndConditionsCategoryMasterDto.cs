namespace DigitalHub.Application.DTOs.Master.TermsAndConditionsCategoryMaster;

public record TermsAndConditionsCategoryMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
