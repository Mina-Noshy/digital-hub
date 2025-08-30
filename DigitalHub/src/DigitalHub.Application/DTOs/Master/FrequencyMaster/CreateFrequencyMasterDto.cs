namespace DigitalHub.Application.DTOs.Master.FrequencyMaster;

public record CreateFrequencyMasterDto : IDto
{
    public string Name { get; set; } = null!;
    public int TotalDays { get; set; }
    public bool IsActive { get; set; }
    public bool IsFixed { get; set; }
}
