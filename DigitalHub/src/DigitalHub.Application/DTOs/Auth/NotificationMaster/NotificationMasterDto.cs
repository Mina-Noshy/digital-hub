using DigitalHub.Domain.Enums;

namespace DigitalHub.Application.DTOs.Auth.NotificationMaster;

public record NotificationMasterDto : IDto
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public NotificationTypes Type { get; set; }
    public string? ActionUrl { get; set; }
    public bool IsRead { get; set; }
    public string? Since { get; set; }
    public DateTime? CreatedAt { get; set; }
}
