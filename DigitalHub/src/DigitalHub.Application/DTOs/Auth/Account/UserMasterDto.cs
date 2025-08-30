namespace DigitalHub.Application.DTOs.Auth.Account;

public record UserMasterDto : IDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public bool IsActive { get; set; } = false;
    public bool IsBlocked { get; set; } = false;
    public string[]? Roles { get; set; }
    public string? ProfileImageUrl { get; set; }
}
