using DigitalHub.Domain.Utilities;

namespace DigitalHub.Domain.Entities.Common;

public class HumanDetails : BaseEntity
{
    // Personal Information
    public string Title { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = null!; // Male, Female, Other
    public string MaritalStatus { get; set; } = null!; // Single, Married, Divorced, Widowed


    // Contact Information
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }


    // Identification Information
    public string? NationalID { get; set; } // e.g., SSN, Passport number, etc.
    public string? PassportNumber { get; set; }
    public string? DriverLicenseNumber { get; set; }


    // Employment/Professional Information
    public string? JobTitle { get; set; }
    public string? Employer { get; set; }
    public string? WorkPhoneNumber { get; set; }
    public string? WorkEmail { get; set; }


    // Social Media / Online Presence
    public string? LinkedIn { get; set; }
    public string? Facebook { get; set; }
    public string? Twitter { get; set; }
    public string? Instagram { get; set; }


    // Emergency Contact 
    public string? EmergencyContactName { get; set; }
    public string? EmergencyContactPhoneNumber { get; set; }
    public string? EmergencyContactRelationship { get; set; }


    // Additional Information
    public string? Notes { get; set; }



    public string FullName
        => $"{FirstName} {LastName}".Trim();
    public string FullNameWithTitle
        => $"{Title} {FullName}".Trim();

    public string Age
    {
        get
        {
            var today = DateTimeProvider.UtcNow;
            var dob = DateOfBirth;

            // Calculate years
            var years = today.Year - dob.Year;

            // Adjust if birthday hasn't occurred this year
            if (today.Month < dob.Month || (today.Month == dob.Month && today.Day < dob.Day))
            {
                years--;
            }

            // Calculate months
            var months = today.Month - dob.Month;
            if (today.Day < dob.Day)
            {
                months--;
            }

            // Adjust months if negative
            if (months < 0)
            {
                months += 12;
            }

            // Calculate days
            var days = today.Day - dob.Day;
            if (days < 0)
            {
                // Get the number of days in the previous month
                var prevMonth = today.AddMonths(-1);
                days += DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
            }

            return $"{years} years, {months} months, {days} days";
        }
    }
}
