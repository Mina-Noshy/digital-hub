namespace DigitalHub.Application.DTOs.Auth.RoleMaster;

public record PageForRolePermissionDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public bool Selected { get; set; }
    public bool Create { get; set; }
    public bool Update { get; set; }
    public bool Delete { get; set; }
    public bool Export { get; set; }
    public bool Print { get; set; }
}
