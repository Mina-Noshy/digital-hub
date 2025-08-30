using DigitalHub.Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHub.Infrastructure.Configurations;

internal sealed class GenderMasterConfigurations : IEntityTypeConfiguration<GenderMaster>
{
    public void Configure(EntityTypeBuilder<GenderMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<GenderMaster>()
            {
               new GenderMaster { Id = 1, Name = "Male" },
                new GenderMaster { Id = 2, Name = "Female"  },
                new GenderMaster { Id = 3, Name = "Other"  }
            };

        entity.HasData(items);
    }
}

internal sealed class MaritalStatusMasterConfigurations : IEntityTypeConfiguration<MaritalStatusMaster>
{
    public void Configure(EntityTypeBuilder<MaritalStatusMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<MaritalStatusMaster>()
            {
                new MaritalStatusMaster { Id = 1, Name = "Single"  },
                new MaritalStatusMaster { Id = 2, Name = "Engaged"  },
                new MaritalStatusMaster { Id = 3, Name = "Married"  },
                new MaritalStatusMaster { Id = 4, Name = "Divorced"  },
                new MaritalStatusMaster { Id = 5, Name = "Widowed"  }
            };

        entity.HasData(items);
    }
}

internal sealed class NationalityMasterConfigurations : IEntityTypeConfiguration<NationalityMaster>
{
    public void Configure(EntityTypeBuilder<NationalityMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<NationalityMaster>()
            {
                new NationalityMaster { Id = 1, Name = "Egyptian"  },
                new NationalityMaster { Id = 2, Name = "Iranian"  },
                new NationalityMaster { Id = 3, Name = "Turkish" },
                new NationalityMaster { Id = 4, Name = "Lebanese"  },
                new NationalityMaster { Id = 5, Name = "Syrian"  },
                new NationalityMaster { Id = 6, Name = "Jordanian"  },
                new NationalityMaster { Id = 7, Name = "Palestinian"  },
                new NationalityMaster { Id = 8, Name = "Iraqi"  },
                new NationalityMaster { Id = 9, Name = "Saudi"  },
                new NationalityMaster { Id = 10, Name = "Yemeni"  },
                new NationalityMaster { Id = 11, Name = "Emirati"  },
                new NationalityMaster { Id = 12, Name = "Qatari"  },
                new NationalityMaster { Id = 13, Name = "Kuwaiti"  },
                new NationalityMaster { Id = 14, Name = "Bahraini"  },
                new NationalityMaster { Id = 15, Name = "Omani"  },
                new NationalityMaster { Id = 16, Name = "Moroccan"  },
                new NationalityMaster { Id = 17, Name = "Tunisian"  },
                new NationalityMaster { Id = 18, Name = "Algerian"  },
                new NationalityMaster { Id = 19, Name = "Libyan"  }
            };

        entity.HasData(items);
    }
}

internal sealed class CountryMasterConfigurations : IEntityTypeConfiguration<CountryMaster>
{
    public void Configure(EntityTypeBuilder<CountryMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<CountryMaster>()
        {
            new CountryMaster { Id = 1, Name = "Egypt", ShortName = "EG", PhoneCode = "+20"  },
            new CountryMaster { Id = 2, Name = "Saudi Arabia", ShortName = "SA", PhoneCode = "+966"  },
            new CountryMaster { Id = 3, Name = "United Arab Emirates", ShortName = "AE", PhoneCode = "+971"  },
            new CountryMaster { Id = 4, Name = "Morocco", ShortName = "MA", PhoneCode = "+212"  },
            new CountryMaster { Id = 5, Name = "Jordan", ShortName = "JO", PhoneCode = "+962"  }
        };

        entity.HasData(items);
    }
}

internal sealed class CityMasterConfigurations : IEntityTypeConfiguration<CityMaster>
{
    public void Configure(EntityTypeBuilder<CityMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => new { x.CountryId, x.Name }).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<CityMaster>()
        {
            // Cities for Egypt
            new CityMaster { Id = 1, CountryId = 1, Name = "Cairo"  },
            new CityMaster { Id = 2, CountryId = 1, Name = "Alexandria"  },
            new CityMaster { Id = 3, CountryId = 1, Name = "Giza"  },
            
            // Cities for Saudi Arabia
            new CityMaster { Id = 4, CountryId = 2, Name = "Riyadh"  },
            new CityMaster { Id = 5, CountryId = 2, Name = "Jeddah"  },
            new CityMaster { Id = 6, CountryId = 2, Name = "Mecca"  },
            
            // Cities for UAE
            new CityMaster { Id = 7, CountryId = 3, Name = "Dubai"  },
            new CityMaster { Id = 8, CountryId = 3, Name = "Abu Dhabi"  },
            new CityMaster { Id = 9, CountryId = 3, Name = "Sharjah"  },
            
            // Cities for Morocco
            new CityMaster { Id = 10, CountryId = 4, Name = "Casablanca"  },
            new CityMaster { Id = 11, CountryId = 4, Name = "Rabat"  },
            new CityMaster { Id = 12, CountryId = 4, Name = "Fes"  },
            
            // Cities for Jordan
            new CityMaster { Id = 13, CountryId = 5, Name = "Amman"  },
            new CityMaster { Id = 14, CountryId = 5, Name = "Zarqa"  },
            new CityMaster { Id = 15, CountryId = 5, Name = "Irbid"  }
        };

        entity.HasData(items);
    }
}

internal sealed class TitleMasterConfigurations : IEntityTypeConfiguration<TitleMaster>
{
    public void Configure(EntityTypeBuilder<TitleMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<TitleMaster>()
        {
            new TitleMaster { Id = 1, Name = "Mr." },
            new TitleMaster { Id = 2, Name = "Mrs."  },
            new TitleMaster { Id = 3, Name = "Ms."  },
            new TitleMaster { Id = 4, Name = "Dr."  },
            new TitleMaster { Id = 5, Name = "M/s."  },
            new TitleMaster { Id = 6, Name = "Eng."  },
        };

        entity.HasData(items);
    }
}

internal sealed class UnitViewMasterConfigurations : IEntityTypeConfiguration<UnitViewMaster>
{
    public void Configure(EntityTypeBuilder<UnitViewMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<UnitViewMaster>()
        {
            new UnitViewMaster { Id = 1, Name = "Sea View" },
            new UnitViewMaster { Id = 2, Name = "Mountain View" },
            new UnitViewMaster { Id = 3, Name = "City View" },
            new UnitViewMaster { Id = 4, Name = "Park View" },
            new UnitViewMaster { Id = 5, Name = "Lake View" },
            new UnitViewMaster { Id = 6, Name = "Garden View" },
            new UnitViewMaster { Id = 7, Name = "Pool View" },
            new UnitViewMaster { Id = 8, Name = "Street View" },
            new UnitViewMaster { Id = 9, Name = "Skyline View" },
            new UnitViewMaster { Id = 10, Name = "Golf Course View" },
            new UnitViewMaster { Id = 11, Name = "Desert View" },
            new UnitViewMaster { Id = 12, Name = "River View" },
        };

        entity.HasData(items);
    }
}

internal sealed class UnitCategoryMasterConfigurations : IEntityTypeConfiguration<UnitCategoryMaster>
{
    public void Configure(EntityTypeBuilder<UnitCategoryMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<UnitCategoryMaster>()
        {
            new UnitCategoryMaster { Id = 1, Name = "Residential" },
            new UnitCategoryMaster { Id = 2, Name = "Commercial" },
            new UnitCategoryMaster { Id = 3, Name = "Retail" },
            new UnitCategoryMaster { Id = 4, Name = "Industrial" },
            new UnitCategoryMaster { Id = 5, Name = "Mixed-Use" },
            new UnitCategoryMaster { Id = 6, Name = "Hospitality" },
            new UnitCategoryMaster { Id = 7, Name = "Agricultural" },
            new UnitCategoryMaster { Id = 8, Name = "Recreational" },
            new UnitCategoryMaster { Id = 9, Name = "Healthcare" },
            new UnitCategoryMaster { Id = 10, Name = "Storage" },
            new UnitCategoryMaster { Id = 11, Name = "Education" },
            new UnitCategoryMaster { Id = 12, Name = "Government" },
            new UnitCategoryMaster { Id = 13, Name = "Special Purpose" },
        };

        entity.HasData(items);
    }
}

internal sealed class UnitClassMasterConfigurations : IEntityTypeConfiguration<UnitClassMaster>
{
    public void Configure(EntityTypeBuilder<UnitClassMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<UnitClassMaster>()
        {
            new UnitClassMaster { Id = 1, Name = "Luxury" },
            new UnitClassMaster { Id = 2, Name = "Standard" },
            new UnitClassMaster { Id = 3, Name = "Economy" },
            new UnitClassMaster { Id = 4, Name = "Premium" },
            new UnitClassMaster { Id = 5, Name = "Deluxe" },
            new UnitClassMaster { Id = 6, Name = "Executive" },
            new UnitClassMaster { Id = 7, Name = "Affordable" },
            new UnitClassMaster { Id = 8, Name = "Budget" },
            new UnitClassMaster { Id = 9, Name = "Serviced" },
            new UnitClassMaster { Id = 10, Name = "Corporate" },
            new UnitClassMaster { Id = 11, Name = "Student" },
            new UnitClassMaster { Id = 12, Name = "Senior Living" },
            new UnitClassMaster { Id = 13, Name = "Vacation Rental" },
        };

        entity.HasData(items);
    }
}

internal sealed class UnitStyleMasterConfigurations : IEntityTypeConfiguration<UnitStyleMaster>
{
    public void Configure(EntityTypeBuilder<UnitStyleMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<UnitStyleMaster>()
        {
            new UnitStyleMaster { Id = 1, Name = "Modern" },
            new UnitStyleMaster { Id = 2, Name = "Contemporary" },
            new UnitStyleMaster { Id = 3, Name = "Traditional" },
            new UnitStyleMaster { Id = 4, Name = "Minimalist" },
            new UnitStyleMaster { Id = 5, Name = "Industrial" },
            new UnitStyleMaster { Id = 6, Name = "Rustic" },
            new UnitStyleMaster { Id = 7, Name = "Scandinavian" },
            new UnitStyleMaster { Id = 8, Name = "Mediterranean" },
            new UnitStyleMaster { Id = 9, Name = "Colonial" },
            new UnitStyleMaster { Id = 10, Name = "Victorian" },
            new UnitStyleMaster { Id = 11, Name = "Art Deco" },
            new UnitStyleMaster { Id = 12, Name = "Farmhouse" },
            new UnitStyleMaster { Id = 13, Name = "Mid-Century Modern" },
            new UnitStyleMaster { Id = 14, Name = "Bohemian" },
            new UnitStyleMaster { Id = 15, Name = "Eclectic" },
            new UnitStyleMaster { Id = 16, Name = "Japanese Zen" },
            new UnitStyleMaster { Id = 17, Name = "Tropical" },
            new UnitStyleMaster { Id = 18, Name = "Transitional" },
            new UnitStyleMaster { Id = 19, Name = "Urban" },
        };

        entity.HasData(items);
    }
}

internal sealed class UnitModelMasterConfigurations : IEntityTypeConfiguration<UnitModelMaster>
{
    public void Configure(EntityTypeBuilder<UnitModelMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<UnitModelMaster>()
        {
            new UnitModelMaster { Id = 1, Name = "Studio Model" },
            new UnitModelMaster { Id = 2, Name = "Penthouse Model" },
            new UnitModelMaster { Id = 3, Name = "Office Model" },
            new UnitModelMaster { Id = 4, Name = "1-Bedroom Model" },
            new UnitModelMaster { Id = 5, Name = "2-Bedroom Model" },
            new UnitModelMaster { Id = 6, Name = "3-Bedroom Model" },
            new UnitModelMaster { Id = 7, Name = "4-Bedroom Model" },
            new UnitModelMaster { Id = 8, Name = "Villa Model" },
            new UnitModelMaster { Id = 9, Name = "Duplex Model" },
            new UnitModelMaster { Id = 10, Name = "Townhouse Model" },
            new UnitModelMaster { Id = 11, Name = "Loft Model" },
            new UnitModelMaster { Id = 12, Name = "Retail Shop Model" },
            new UnitModelMaster { Id = 13, Name = "Industrial Model" },
            new UnitModelMaster { Id = 14, Name = "Warehouse Model" },
            new UnitModelMaster { Id = 15, Name = "Shared Office Model" },
            new UnitModelMaster { Id = 16, Name = "Serviced Apartment Model" },
            new UnitModelMaster { Id = 17, Name = "Studio Plus Model" },
            new UnitModelMaster { Id = 18, Name = "Penthouse Suite Model" },
            new UnitModelMaster { Id = 19, Name = "Large Office Model" },
            new UnitModelMaster { Id = 20, Name = "Shared Living Model" },
        };

        entity.HasData(items);
    }
}

internal sealed class UnitTypeMasterConfigurations : IEntityTypeConfiguration<UnitTypeMaster>
{
    public void Configure(EntityTypeBuilder<UnitTypeMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<UnitTypeMaster>()
        {
            new UnitTypeMaster { Id = 1, Name = "Residential" },
            new UnitTypeMaster { Id = 2, Name = "Commercial" },
            new UnitTypeMaster { Id = 3, Name = "Industrial" },
            new UnitTypeMaster { Id = 4, Name = "Retail" },
            new UnitTypeMaster { Id = 5, Name = "Hospitality" },
            new UnitTypeMaster { Id = 6, Name = "Warehouse" },
            new UnitTypeMaster { Id = 7, Name = "Office" },
            new UnitTypeMaster { Id = 8, Name = "Mixed-Use" },
            new UnitTypeMaster { Id = 9, Name = "Educational" },
            new UnitTypeMaster { Id = 10, Name = "Healthcare" },
            new UnitTypeMaster { Id = 11, Name = "Recreational" },
            new UnitTypeMaster { Id = 12, Name = "Agricultural" },
            new UnitTypeMaster { Id = 13, Name = "Vacant Land" },
            new UnitTypeMaster { Id = 14, Name = "Government" },
            new UnitTypeMaster { Id = 15, Name = "Religious" },
            new UnitTypeMaster { Id = 16, Name = "Community Center" },
            new UnitTypeMaster { Id = 17, Name = "Parking Space" },
        };

        entity.HasData(items);
    }
}

internal sealed class PropertyCategoryMasterConfigurations : IEntityTypeConfiguration<PropertyCategoryMaster>
{
    public void Configure(EntityTypeBuilder<PropertyCategoryMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<PropertyCategoryMaster>()
        {
            new PropertyCategoryMaster { Id = 1, Name = "Residential" },
            new PropertyCategoryMaster { Id = 2, Name = "Commercial" },
        };

        entity.HasData(items);
    }
}

internal sealed class PropertyTypeMasterConfigurations : IEntityTypeConfiguration<PropertyTypeMaster>
{
    public void Configure(EntityTypeBuilder<PropertyTypeMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<PropertyTypeMaster>()
        {
            new PropertyTypeMaster { Id = 1, Name = "Residential" },
            new PropertyTypeMaster { Id = 2, Name = "Commercial" },
            new PropertyTypeMaster { Id = 3, Name = "Industrial" },
        };

        entity.HasData(items);
    }
}

internal sealed class TermsAndConditionsCategoryMasterConfigurations : IEntityTypeConfiguration<TermsAndConditionsCategoryMaster>
{
    public void Configure(EntityTypeBuilder<TermsAndConditionsCategoryMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<TermsAndConditionsCategoryMaster>()
        {
            new TermsAndConditionsCategoryMaster { Id = 1, Name = "Lease and Ownership Terms" },
            new TermsAndConditionsCategoryMaster { Id = 2, Name = "Payment Terms" },
            new TermsAndConditionsCategoryMaster { Id = 3, Name = "Maintenance and Repair Terms" },
            new TermsAndConditionsCategoryMaster { Id = 4, Name = "Termination or Cancellation Terms" },
            new TermsAndConditionsCategoryMaster { Id = 5, Name = "Usage Terms" },
            new TermsAndConditionsCategoryMaster { Id = 6, Name = "Insurance Terms" },
            new TermsAndConditionsCategoryMaster { Id = 7, Name = "Warranty Terms" },
            new TermsAndConditionsCategoryMaster { Id = 8, Name = "Legal Liability Terms" },
            new TermsAndConditionsCategoryMaster { Id = 9, Name = "Dispute Resolution Terms" },
            new TermsAndConditionsCategoryMaster { Id = 10, Name = "Modification or Update Terms" },
        };

        entity.HasData(items);
    }
}

internal sealed class TermsAndConditionsMasterConfigurations : IEntityTypeConfiguration<TermsAndConditionsMaster>
{
    public void Configure(EntityTypeBuilder<TermsAndConditionsMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => new { x.CategoryId, x.Description }).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<TermsAndConditionsMaster>()
        {
            // Lease and Ownership Terms
            new TermsAndConditionsMaster { Id = 1, CategoryId = 1, Description = "The tenant must comply with the lease agreement terms as outlined in the contract." },
            new TermsAndConditionsMaster { Id = 2, CategoryId = 1, Description = "Ownership rights are retained by the property owner unless explicitly transferred in writing." },
            
            // Payment Terms
            new TermsAndConditionsMaster { Id = 3, CategoryId = 2, Description = "Rent payments must be made by the 5th of each month to avoid late fees." },
            new TermsAndConditionsMaster { Id = 4, CategoryId = 2, Description = "Security deposits are refundable subject to property condition upon lease termination." },
            
            // Maintenance and Repair Terms
            new TermsAndConditionsMaster { Id = 5, CategoryId = 3, Description = "Tenants are responsible for minor repairs up to $50." },
            new TermsAndConditionsMaster { Id = 6, CategoryId = 3, Description = "Major structural repairs will be handled by the property owner." },
            
            // Termination or Cancellation Terms
            new TermsAndConditionsMaster { Id = 7, CategoryId = 4, Description = "A 30-day notice is required for lease termination by either party." },
            new TermsAndConditionsMaster { Id = 8, CategoryId = 4, Description = "Early termination fees apply if the lease is broken before the agreed term." },
            
            // Usage Terms
            new TermsAndConditionsMaster { Id = 9, CategoryId = 5, Description = "The property may only be used for residential purposes unless stated otherwise." },
            new TermsAndConditionsMaster { Id = 10, CategoryId = 5, Description = "Subleasing the property without written permission is prohibited." },
            
            // Insurance Terms
            new TermsAndConditionsMaster { Id = 11, CategoryId = 6, Description = "Tenants are advised to obtain renters insurance for personal belongings." },
            new TermsAndConditionsMaster { Id = 12, CategoryId = 6, Description = "Property owner maintains insurance for structural damages only." },
            
            // Warranty Terms
            new TermsAndConditionsMaster { Id = 13, CategoryId = 7, Description = "Appliances provided in the rental are covered under a one-year warranty." },
            new TermsAndConditionsMaster { Id = 14, CategoryId = 7, Description = "Warranty claims must be reported within 30 days of the issue occurring." },
            
            // Legal Liability Terms
            new TermsAndConditionsMaster { Id = 15, CategoryId = 8, Description = "Property owner is not liable for injuries resulting from tenant negligence." },
            new TermsAndConditionsMaster { Id = 16, CategoryId = 8, Description = "Tenants must comply with local laws and ordinances while occupying the property." },
            
            // Dispute Resolution Terms
            new TermsAndConditionsMaster { Id = 17, CategoryId = 9, Description = "All disputes shall be resolved through arbitration before legal action." },
            new TermsAndConditionsMaster { Id = 18, CategoryId = 9, Description = "Legal disputes must be filed in the jurisdiction where the property is located." },
            
            // Modification or Update Terms
            new TermsAndConditionsMaster { Id = 19, CategoryId = 10, Description = "Terms and conditions are subject to change with a 30-day notice to tenants." },
            new TermsAndConditionsMaster { Id = 20, CategoryId = 10, Description = "Any modifications to the lease agreement must be agreed upon in writing by both parties." },
        };

        entity.HasData(items);
    }
}

internal sealed class CommunicationMethodMasterConfigurations : IEntityTypeConfiguration<CommunicationMethodMaster>
{
    public void Configure(EntityTypeBuilder<CommunicationMethodMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<CommunicationMethodMaster>()
        {
           new CommunicationMethodMaster { Id = 1, Name = "Email" },
           new CommunicationMethodMaster { Id = 2, Name = "Phone"  },
           new CommunicationMethodMaster { Id = 3, Name = "Whatsapp"  },
           new CommunicationMethodMaster { Id = 4, Name = "SMS"  },
           new CommunicationMethodMaster { Id = 5, Name = "Fax"  },
        };

        entity.HasData(items);
    }
}

internal sealed class PriorityMasterConfigurations : IEntityTypeConfiguration<PriorityMaster>
{
    public void Configure(EntityTypeBuilder<PriorityMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<PriorityMaster>()
        {
            new PriorityMaster { Id = 1, Name = "Low" },
            new PriorityMaster { Id = 2, Name = "Medium" },
            new PriorityMaster { Id = 3, Name = "High" },
        };

        entity.HasData(items);
    }
}

internal sealed class FloorMasterConfigurations : IEntityTypeConfiguration<FloorMaster>
{
    public void Configure(EntityTypeBuilder<FloorMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<FloorMaster>();

        for (int i = 1; i <= 100; i++)
        {
            items.Add(new FloorMaster()
            {
                Id = i,
                Name = $"Floor {i}"
            });
        }

        entity.HasData(items);
    }
}

internal sealed class FrequencyMasterConfigurations : IEntityTypeConfiguration<FrequencyMaster>
{
    public void Configure(EntityTypeBuilder<FrequencyMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Name).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<FrequencyMaster>
        {
            new FrequencyMaster { Id = 1, Name = "OneTime", TotalDays = 0, IsActive = true, IsFixed = false },
            new FrequencyMaster { Id = 2, Name = "Daily", TotalDays = 1, IsActive = true, IsFixed = false },
            new FrequencyMaster { Id = 3, Name = "Weekly", TotalDays = 7, IsActive = true, IsFixed = false },
            new FrequencyMaster { Id = 4, Name = "BiWeekly", TotalDays = 14, IsActive = true, IsFixed = false },
            new FrequencyMaster { Id = 5, Name = "Monthly", TotalDays = 30, IsActive = true, IsFixed = true },
            new FrequencyMaster { Id = 6, Name = "BiMonthly", TotalDays = 60, IsActive = true, IsFixed = true },
            new FrequencyMaster { Id = 7, Name = "Quarterly", TotalDays = 90, IsActive = true, IsFixed = true },
            new FrequencyMaster { Id = 8, Name = "SemiAnnually", TotalDays = 180, IsActive = true, IsFixed = true },
            new FrequencyMaster { Id = 9, Name = "Annually", TotalDays = 365, IsActive = true, IsFixed = true },
            new FrequencyMaster { Id = 10, Name = "Biennially", TotalDays = 730, IsActive = true, IsFixed = true },
            new FrequencyMaster { Id = 11, Name = "Triennially", TotalDays = 1095, IsActive = true, IsFixed = true },
        };

        entity.HasData(items);
    }
}

internal sealed class BankMasterConfigurations : IEntityTypeConfiguration<BankMaster>
{
    public void Configure(EntityTypeBuilder<BankMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.BankCode).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<BankMaster>
        {
            new BankMaster { Id = 1, BankName = "Bank of America", BankCode = "BOA001", RoutingNumber = "121000358", Branch = "Main Branch", SwiftCode = "BOFAUS3N", Country = "USA", City = "New York", Address = "123 Wall Street" },
            new BankMaster { Id = 2, BankName = "Wells Fargo", BankCode = "WF002", RoutingNumber = "121042882", Branch = "Downtown", SwiftCode = "WFBIUS6S", Country = "USA", City = "San Francisco", Address = "420 Montgomery Street" },
            new BankMaster { Id = 3, BankName = "HSBC", BankCode = "HSBC003", RoutingNumber = "022000020", Branch = "London HQ", SwiftCode = "HSBCGB2L", Country = "UK", City = "London", Address = "8 Canada Square" },
            new BankMaster { Id = 4, BankName = "Citibank", BankCode = "CITI004", RoutingNumber = "021000089", Branch = "Global Markets", SwiftCode = "CITIUS33", Country = "USA", City = "New York", Address = "388 Greenwich Street" },
            new BankMaster { Id = 5, BankName = "Deutsche Bank", BankCode = "DB005", RoutingNumber = "026002481", Branch = "Frankfurt HQ", SwiftCode = "DEUTDEFF", Country = "Germany", City = "Frankfurt", Address = "Taunusanlage 12" }
        };

        entity.HasData(items);
    }
}

