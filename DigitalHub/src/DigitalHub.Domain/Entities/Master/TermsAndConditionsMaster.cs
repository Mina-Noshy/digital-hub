using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalHub.Domain.Entities.Master;

public class TermsAndConditionsMaster : BaseEntity
{
    public long CategoryId { get; set; }
    public string Description { get; set; } = null!;
    public bool IsRequired { get; set; }



    [ForeignKey(nameof(CategoryId))]
    public virtual TermsAndConditionsCategoryMaster GetCategory { get; set; } = null!;
}
