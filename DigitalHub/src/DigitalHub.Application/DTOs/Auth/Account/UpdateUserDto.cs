namespace DigitalHub.Application.DTOs.Auth.Account;

public record UpdateUserDto : IDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public string[]? Roles { get; set; }
}
