namespace DigitalHub.Application.DTOs.Auth.RoleMaster;

public record MenuForRolePermissionDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public IEnumerable<PageForRolePermissionDto>? Pages { get; set; }
}
