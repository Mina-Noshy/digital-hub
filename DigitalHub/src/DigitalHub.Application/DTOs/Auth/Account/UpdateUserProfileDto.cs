using Microsoft.AspNetCore.Http;

namespace DigitalHub.Application.DTOs.Auth.Account;

public record UpdateUserProfileDto : IDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public IFormFile? ProfileImage { get; set; }
}
