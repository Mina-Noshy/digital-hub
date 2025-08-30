namespace DigitalHub.Application.DTOs.Auth.Auth;

public record UserInfoDto : IDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime TokenExpiration { get; set; }
    public string? ProfileImageUrl { get; set; }
    public List<string>? Roles { get; set; }

}
