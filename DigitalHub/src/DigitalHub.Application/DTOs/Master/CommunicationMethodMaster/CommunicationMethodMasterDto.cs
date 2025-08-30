namespace DigitalHub.Application.DTOs.Master.CommunicationMethodMaster;

public record CommunicationMethodMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}
