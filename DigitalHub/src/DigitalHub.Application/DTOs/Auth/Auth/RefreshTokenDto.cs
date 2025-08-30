namespace DigitalHub.Application.DTOs.Auth.Auth;

public record RefreshTokenDto : IDto
{
    public string Token { get; set; } = null!;
}
