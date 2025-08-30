using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalHub.Domain.Entities.Master;

public class CityMaster : BaseEntity
{
    public long CountryId { get; set; }
    public string Name { get; set; } = null!;


    [ForeignKey(nameof(CountryId))]
    public virtual CountryMaster GetCountry { get; set; } = null!;
}