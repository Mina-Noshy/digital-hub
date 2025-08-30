namespace DigitalHub.Application.DTOs.Auth.RolePageMaster;

public record UserModuleMenuDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Label { get; set; }
    public string? Icon { get; set; }
    public string? I18nKey { get; set; }
    public int? SortOrder { get; set; }

    public IEnumerable<UserModuleMenuPageDto>? Pages { get; set; }
}

