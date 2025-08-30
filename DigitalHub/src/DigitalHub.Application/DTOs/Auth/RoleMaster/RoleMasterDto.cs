using DigitalHub.Domain.Enums;

namespace DigitalHub.Application.DTOs.Auth.RoleMaster;

public record RoleMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string NormalizedName { get; set; } = null!;
    public string? ConcurrencyStamp { get; set; }
    public UserTypes? DefaultFor { get; set; }
}
