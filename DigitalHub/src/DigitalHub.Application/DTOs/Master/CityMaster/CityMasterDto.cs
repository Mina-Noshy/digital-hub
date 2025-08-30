namespace DigitalHub.Application.DTOs.Master.CityMaster;

public record CityMasterDto : IDto
{
    public long Id { get; set; }
    public long CountryId { get; set; }
    public string Country { get; set; } = null!;
    public string Name { get; set; } = null!;
}
