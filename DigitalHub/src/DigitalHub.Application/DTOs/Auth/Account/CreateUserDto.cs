using DigitalHub.Domain.Enums;

namespace DigitalHub.Application.DTOs.Auth.Account;

public record CreateUserDto : IDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserTypes UserType { get; set; }

    public string[]? Roles { get; set; }
}
