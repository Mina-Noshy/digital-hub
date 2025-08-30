namespace DigitalHub.Domain.Entities.Master;

public class FrequencyMaster : BaseEntity
{
    public string Name { get; set; } = null!;
    public int TotalDays { get; set; }
    public bool IsActive { get; set; }
    public bool IsFixed { get; set; }
}

/*
    OneTime,        // = 0,
    Daily,          // = 1,
    Weekly,         // = 7,
    BiWeekly,       // = 14,
    Monthly,        // = 30,
    BiMonthly,      // = 60,
    Quarterly,      // = 90,
    SemiAnnually,   // = 180,
    Annually,       // = 365,
    Biennially,     // = 730,
    Triennially,    // = 1095
*/