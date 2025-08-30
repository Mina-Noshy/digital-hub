namespace DigitalHub.Application.DTOs.Master.CommunicationMethodMaster;

public record CreateCommunicationMethodMasterDto : IDto
{
    public string Name { get; set; } = null!;
}
