namespace DigitalHub.Application.DTOs.Auth.RoleMaster;

public record CreateRolePageItemDto : IDto
{
    public long PageId { get; set; }

    public bool Create { get; set; }
    public bool Update { get; set; }
    public bool Delete { get; set; }
    public bool Export { get; set; }
    public bool Print { get; set; }
}
