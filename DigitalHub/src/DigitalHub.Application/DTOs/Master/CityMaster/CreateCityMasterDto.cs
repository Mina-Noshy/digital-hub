namespace DigitalHub.Application.DTOs.Master.CityMaster;

public record CreateCityMasterDto : IDto
{
    public long CountryId { get; set; }
    public string Name { get; set; } = null!;
}
