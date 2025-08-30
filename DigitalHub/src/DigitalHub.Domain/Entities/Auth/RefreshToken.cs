using DigitalHub.Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DigitalHub.Domain.Entities.Auth;

[Owned]
public class RefreshToken
{
    [Key]
    public long Id { get; set; }
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public bool IsExpired => DateTimeProvider.UtcNow >= ExpiresAt;
    public DateTime CreatedAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public bool IsActive => RevokedAt == null && !IsExpired;

}
