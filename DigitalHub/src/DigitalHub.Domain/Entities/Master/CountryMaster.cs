namespace DigitalHub.Domain.Entities.Master;

public class CountryMaster : BaseEntity
{
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!;
    public string PhoneCode { get; set; } = null!;
}
