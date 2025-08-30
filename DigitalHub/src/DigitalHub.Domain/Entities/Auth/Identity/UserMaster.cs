using Microsoft.AspNetCore.Identity;

namespace DigitalHub.Domain.Entities.Auth.Identity;

public class UserMaster : IdentityUser<long>
{
    public string? ProfileImage { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public bool IsActive { get; set; } = false;
    public bool IsBlocked { get; set; } = false;

    public virtual List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public string FullName
        => $"{FirstName} {LastName}".Trim();

    //public string FullNameWithTitle
    //    => $"{Title} {FirstName} {LastName}".Trim();
}
