using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using Microsoft.AspNetCore.Identity;

namespace DigitalHub.Infrastructure.Repositories.Auth;

internal class AuthRepository(UserManager<UserMaster> _userManager, IUnitOfWork _unitOfWork) : IAuthRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public async Task<IdentityResult> AddUserRoleAsync(UserMaster user, string role, CancellationToken cancellationToken = default)
        => await _userManager.AddToRoleAsync(user, role);

    public async Task<IdentityResult> RemoveUserRoleAsync(UserMaster user, string role, CancellationToken cancellationToken = default)
        => await _userManager.RemoveFromRoleAsync(user, role);

    public async Task<IdentityResult> ChangePasswordAsync(UserMaster user, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
        => await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

    public async Task<bool> CheckPasswordAsync(UserMaster user, string password, CancellationToken cancellationToken = default)
        => await _userManager.CheckPasswordAsync(user, password);

}
