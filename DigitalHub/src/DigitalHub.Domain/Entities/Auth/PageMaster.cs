using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalHub.Domain.Entities.Auth;

public class PageMaster : BaseEntity
{
    public long MenuId { get; set; }
    public string Name { get; set; } = null!;
    public string Label { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public string? I18nKey { get; set; }
    public int SortOrder { get; set; }



    [ForeignKey(nameof(MenuId))]
    public virtual MenuMaster GetMenu { get; set; } = null!;


    public virtual ICollection<RolePageMaster>? GetRolePages { get; set; }
}