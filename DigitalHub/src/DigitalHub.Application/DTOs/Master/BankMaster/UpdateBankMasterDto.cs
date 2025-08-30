namespace DigitalHub.Application.DTOs.Master.BankMaster;

public record UpdateBankMasterDto : IDto
{
    public long Id { get; set; }
    public string BankName { get; set; } = null!;
    public string? BankCode { get; set; }
    public string RoutingNumber { get; set; } = null!;
    public string Branch { get; set; } = null!;
    public string? SwiftCode { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string? Address { get; set; }
}
