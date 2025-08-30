namespace DigitalHub.Application.DTOs.Auth.PageMaster;

public record PageMasterDto : IDto
{
    public long Id { get; set; }
    public long MenuId { get; set; }
    public string? Menu { get; set; }
    public string? Module { get; set; }
    public string? Name { get; set; }
    public string? Label { get; set; }
    public string? Path { get; set; }
    public string? Icon { get; set; }
    public string? I18nKey { get; set; }
    public int SortOrder { get; set; }
}
