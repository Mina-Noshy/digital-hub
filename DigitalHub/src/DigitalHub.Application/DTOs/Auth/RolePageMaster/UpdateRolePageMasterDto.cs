namespace DigitalHub.Application.DTOs.Auth.RolePageMaster;

public record UpdateRolePageMasterDto : IDto
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    public long PageId { get; set; }

    public bool Create { get; set; }
    public bool Update { get; set; }
    public bool Delete { get; set; }
    public bool Export { get; set; }
    public bool Print { get; set; }
}
