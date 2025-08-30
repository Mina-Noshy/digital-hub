namespace DigitalHub.Application.DTOs.Auth.PageMaster;

public record UpdatePageMasterDto : IDto
{
    public long Id { get; set; }
    public long MenuId { get; set; }
    public string Name { get; set; } = null!;
    public string Label { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public string? I18nKey { get; set; }
    public int SortOrder { get; set; }
}
