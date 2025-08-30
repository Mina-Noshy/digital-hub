namespace DigitalHub.Application.DTOs.HR.EmployeeMaster;

public record CreateEmployeeMasterDto : IDto
{
    public long DepartmentId { get; set; }
    public long RoleId { get; set; }
    public long EmploymentTypeId { get; set; }
    public DateTime HireDate { get; set; }

    // Personal Information
    public string Title { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public decimal Salary { get; set; }
    public string Currency { get; set; } = null!;
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
}
