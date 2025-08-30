namespace DigitalHub.Domain.Entities.Master;

public class BankMaster : BaseEntity
{
    public string BankName { get; set; } = null!;
    public string? BankCode { get; set; }
    public string RoutingNumber { get; set; } = null!;
    public string Branch { get; set; } = null!;
    public string? SwiftCode { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string? Address { get; set; }
}
