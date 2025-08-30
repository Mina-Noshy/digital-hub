namespace DigitalHub.Application.DTOs.Auth.MenuMaster;

public record UpdateMenuMasterDto : IDto
{
    public long Id { get; set; }
    public long ModuleId { get; set; }
    public string Name { get; set; } = null!;
    public string Label { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public string? I18nKey { get; set; }
    public int SortOrder { get; set; }
}
