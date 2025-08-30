namespace DigitalHub.Application.DTOs.Auth.CompanyDatabaseMaster;

public record CompanyDatabaseMasterDto : IDto
{
    public string DatabaseNo { get; set; } = null!;
    public string CompanyKey { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string? Email { get; set; }
    public string? ContactNo { get; set; }
    public string? LogoUrl { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Addrss { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
    public bool ForCustomers { get; set; }
}
