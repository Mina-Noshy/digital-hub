namespace DigitalHub.Application.DTOs.Master.TermsAndConditionsCategoryMaster;

public record CreateTermsAndConditionsCategoryMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
