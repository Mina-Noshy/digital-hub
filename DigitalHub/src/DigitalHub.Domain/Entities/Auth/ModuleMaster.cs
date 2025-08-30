namespace DigitalHub.Domain.Entities.Auth;

public class ModuleMaster : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Label { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string? Description { get; set; }
    public string? I18nKey { get; set; }

    public string? FrontColor { get; set; } // To change the front-end style base on eatch module
    public string? BackColor { get; set; } // To change the front-end style base on eatch module
    public string? Background { get; set; } // To change the front-end style base on eatch module
    public int SortOrder { get; set; }

    public virtual ICollection<MenuMaster>? GetMenus { get; set; }
}
