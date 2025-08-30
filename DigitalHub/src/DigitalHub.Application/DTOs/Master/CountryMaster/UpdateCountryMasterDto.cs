namespace DigitalHub.Application.DTOs.Master.CountryMaster;

public record UpdateCountryMasterDto : IDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!;
    public string PhoneCode { get; set; } = null!;
}
