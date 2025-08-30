using DigitalHub.Domain.Entities.Auth.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalHub.Domain.Entities.Auth;

public class RolePageMaster : BaseEntity
{
    public long RoleId { get; set; }
    public long PageId { get; set; }

    public bool Create { get; set; }
    public bool Update { get; set; }
    public bool Delete { get; set; }
    public bool Export { get; set; }
    public bool Print { get; set; }


    [ForeignKey(nameof(RoleId))]
    public virtual RoleMaster GetRole { get; set; } = null!;

    [ForeignKey(nameof(PageId))]
    public virtual PageMaster GetPage { get; set; } = null!;
}