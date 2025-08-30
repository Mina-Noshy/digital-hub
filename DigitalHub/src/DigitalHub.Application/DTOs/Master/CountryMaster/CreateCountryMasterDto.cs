namespace DigitalHub.Application.DTOs.Master.CountryMaster;

public record CreateCountryMasterDto : IDto
{
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!;
    public string PhoneCode { get; set; } = null!;
}
