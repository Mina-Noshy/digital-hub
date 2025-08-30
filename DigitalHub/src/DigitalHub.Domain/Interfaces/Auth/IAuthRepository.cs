using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Interfaces.Common.Context;
using Microsoft.AspNetCore.Identity;

namespace DigitalHub.Domain.Interfaces.Auth;

public interface IAuthRepository : IBaseRepository
{
    Task<IdentityResult> AddUserRoleAsync(UserMaster user, string role, CancellationToken cancellationToken = default);
    Task<IdentityResult> RemoveUserRoleAsync(UserMaster user, string role, CancellationToken cancellationToken = default);
    Task<IdentityResult> ChangePasswordAsync(UserMaster user, string currentPassword, string newPassword, CancellationToken cancellationToken = default);
    Task<bool> CheckPasswordAsync(UserMaster user, string password, CancellationToken cancellationToken = default);

}
