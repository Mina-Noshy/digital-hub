namespace DigitalHub.Application.DTOs.Auth.RoleMaster;

public record ModuleForRolePermissionDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public IEnumerable<MenuForRolePermissionDto>? Menus { get; set; }
}
