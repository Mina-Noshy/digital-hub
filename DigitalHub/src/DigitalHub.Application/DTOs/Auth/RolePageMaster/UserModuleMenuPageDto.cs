namespace DigitalHub.Application.DTOs.Auth.RolePageMaster;

public record UserModuleMenuPageDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Label { get; set; }
    public string? Path { get; set; }
    public string? Icon { get; set; }
    public string? I18nKey { get; set; }
    public int? SortOrder { get; set; }

    public bool Create { get; set; }
    public bool Update { get; set; }
    public bool Delete { get; set; }
    public bool Export { get; set; }
    public bool Print { get; set; }
}
