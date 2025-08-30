namespace DigitalHub.Application.DTOs.Auth.Auth;

public record GetTokenDto : IDto
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}
