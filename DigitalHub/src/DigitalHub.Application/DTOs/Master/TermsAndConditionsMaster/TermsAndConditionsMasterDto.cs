namespace DigitalHub.Application.DTOs.Master.TermsAndConditionsMaster;

public record TermsAndConditionsMasterDto : IDto
{
    public long Id { get; set; }
    public long CategoryId { get; set; }
    public string Category { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsRequired { get; set; }
}
