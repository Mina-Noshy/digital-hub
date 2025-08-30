namespace DigitalHub.Domain.Entities.Auth;

public class EntityChangeLog : BaseEntity
{
    public long UserId { get; set; }
    public long EntityId { get; set; }

    public string EntityName { get; set; } = null!;
    public string Operation { get; set; } = null!;
    public string OldValues { get; set; } = null!;
    public string NewValues { get; set; } = null!;
    public string IPAddress { get; set; } = null!;
    public string UserAgent { get; set; } = null!;
    public string ChangedBy { get; set; } = null!;
    public DateTime Timestamp { get; set; }
}
