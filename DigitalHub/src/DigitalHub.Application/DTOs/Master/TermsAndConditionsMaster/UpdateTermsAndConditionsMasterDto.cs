namespace DigitalHub.Application.DTOs.Master.TermsAndConditionsMaster;

public record UpdateTermsAndConditionsMasterDto : IDto
{
    public long Id { get; set; }
    public long CategoryId { get; set; }
    public string Description { get; set; } = null!;
    public bool IsRequired { get; set; }
}
