namespace DigitalHub.Application.DTOs.Auth.UserLoginHistoryMaster;

public record UserLoginHistoryMasterDto : IDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public DateTime LoginDate { get; set; }
    public string IpAddress { get; set; } = null!;
    public string UserAgent { get; set; } = null!;
    public string LoginType { get; set; } = null!; // e.g., "Local", "Google", "Facebook"
}
