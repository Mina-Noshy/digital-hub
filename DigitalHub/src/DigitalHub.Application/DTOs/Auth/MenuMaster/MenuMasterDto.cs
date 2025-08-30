namespace DigitalHub.Application.DTOs.Auth.MenuMaster;

public record MenuMasterDto : IDto
{
    public long Id { get; set; }
    public long? ModuleId { get; set; }
    public string? Module { get; set; }
    public string? Name { get; set; }
    public string? Label { get; set; }
    public string? Icon { get; set; }
    public string? I18nKey { get; set; }
    public int SortOrder { get; set; }
}
