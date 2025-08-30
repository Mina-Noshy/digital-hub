using DigitalHub.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace DigitalHub.Domain.Entities.Auth.Identity;

public class RoleMaster : IdentityRole<long>
{
    public UserTypes? DefaultFor { get; set; }
}
