namespace DigitalHub.Domain.Entities.Master;

public class TermsAndConditionsCategoryMaster : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<TermsAndConditionsMaster>? GetTermsAndConditions { get; set; }
}
