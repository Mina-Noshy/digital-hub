using System.ComponentModel.DataAnnotations;

namespace DigitalHub.Application.DTOs.Auth.Auth;

public record ChangePasswordDto : IDto
{
    [Required]
    public string CurrentPassword { get; set; } = null!;
    [MaxLength(100)]
    [MinLength(6)]
    public string NewPassword { get; set; } = null!;
}
