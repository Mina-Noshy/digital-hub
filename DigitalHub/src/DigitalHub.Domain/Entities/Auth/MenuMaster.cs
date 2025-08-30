using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalHub.Domain.Entities.Auth;

public class MenuMaster : BaseEntity
{
    public long ModuleId { get; set; }
    public string Name { get; set; } = null!;
    public string Label { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public string? I18nKey { get; set; }
    public int SortOrder { get; set; }


    [ForeignKey(nameof(ModuleId))]
    public virtual ModuleMaster? GetModule { get; set; }


    public virtual ICollection<PageMaster>? GetPages { get; set; }
}