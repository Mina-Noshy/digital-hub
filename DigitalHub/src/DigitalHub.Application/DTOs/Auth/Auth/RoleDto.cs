namespace DigitalHub.Application.DTOs.Auth.Auth;

public record RoleDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string NormalizedName { get; set; } = null!;
    public string? ConcurrencyStamp { get; set; }
}
