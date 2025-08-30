namespace DigitalHub.Application.DTOs.Auth.RolePageMaster;

public record RolePageMasterDto : IDto
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    public long PageId { get; set; }

    public string? Role { get; set; }
    public string? Page { get; set; }
    public string? Menu { get; set; }
    public string? Module { get; set; }

    public bool Create { get; set; }
    public bool Update { get; set; }
    public bool Delete { get; set; }
    public bool Export { get; set; }
    public bool Print { get; set; }
}
