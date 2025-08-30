using DigitalHub.Domain.Enums;

namespace DigitalHub.Domain.Entities.Auth;

public class NotificationMaster : BaseEntity
{
    public long UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public NotificationTypes Type { get; set; }
    public string? ActionUrl { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }

}
