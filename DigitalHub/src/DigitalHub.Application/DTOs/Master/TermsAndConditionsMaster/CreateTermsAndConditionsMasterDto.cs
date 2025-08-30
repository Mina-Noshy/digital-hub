namespace DigitalHub.Application.DTOs.Master.TermsAndConditionsMaster;

public record CreateTermsAndConditionsMasterDto : IDto
{
    public long CategoryId { get; set; }
    public string Description { get; set; } = null!;
    public bool IsRequired { get; set; }
}
