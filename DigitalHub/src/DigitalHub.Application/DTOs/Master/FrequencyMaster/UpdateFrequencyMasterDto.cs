namespace DigitalHub.Application.DTOs.Master.FrequencyMaster;

public record UpdateFrequencyMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public int TotalDays { get; set; }
    public bool IsActive { get; set; }
    public bool IsFixed { get; set; }
}
