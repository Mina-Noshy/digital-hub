namespace DigitalHub.Application.DTOs.Master.CityMaster;

public record UpdateCityMasterDto : IDto
{
    public long Id { get; set; }
    public long CountryId { get; set; }
    public string Name { get; set; } = null!;
}
