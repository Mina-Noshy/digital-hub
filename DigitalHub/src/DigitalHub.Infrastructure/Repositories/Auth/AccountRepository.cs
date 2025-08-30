using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;
using DigitalHub.Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace DigitalHub.Infrastructure.Repositories.Auth;

internal class AccountRepository(UserManager<UserMaster> _userManager, IUnitOfWork _unitOfWork) : IAccountRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public IQueryable<UserMaster> GetUsersAsDropdown(DropdownQueryParams queryParams)
    {
        return _userManager.Users
            .Where(x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) ||
                (x.FirstName + " " + x.LastName + " " + x.Email + " " + x.PhoneNumber + " " + x.UserName).Contains(queryParams.SearchTerm))
            .OrderBy(o => o.UserName).ThenBy(o => o.LastName)
            .Take(queryParams.Limit);
    }

    public async Task<bool> CreateUserAsync(UserMaster user, string password, CancellationToken cancellationToken = default)
        => await ConfigureAndCreateUser(user, password, cancellationToken);

    public async Task<bool> UpdateUserAsync(UserMaster user, CancellationToken cancellationToken = default)
    {
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(result.Errors.FirstOrDefault()?.Description);
        }

        return result.Succeeded;
    }

    public async Task<bool> DeleteUserAsync(UserMaster user, CancellationToken cancellationToken = default)
    {
        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(result.Errors.FirstOrDefault()?.Description);
        }

        return result.Succeeded;
    }

    public async Task<IEnumerable<UserMaster>> GetUsersPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken = default)
    {
        var orderByExpression = queryParams.Ascending ? queryParams.SortColumn : $"{queryParams.SortColumn} DESC";

        return await _userManager.Users
            .Where(x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) ||
                (x.FirstName + " " + x.LastName + " " + x.Email + " " + x.PhoneNumber + " " + x.UserName).Contains(queryParams.SearchTerm))
            .OrderBy(orderByExpression)
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetUsersCountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken = default)
    {
        return await _userManager.Users
            .Where(x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) ||
                (x.FirstName + " " + x.LastName + " " + x.Email + " " + x.PhoneNumber + " " + x.UserName).Contains(queryParams.SearchTerm))
            .CountAsync(cancellationToken);
    }

    public async Task<bool> ConfirmEmailAsync(long userId, string token, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return false;
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> ToggleEmailConfirmationAsync(long userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return false;
        }

        user.EmailConfirmed = !user.EmailConfirmed;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> ToggleContactNumberConfirmationAsync(long userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return false;
        }

        user.PhoneNumberConfirmed = !user.PhoneNumberConfirmed;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> ToggleUserActiveStatusAsync(long userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return false;
        }

        user.IsActive = !user.IsActive;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> ToggleUserBlockStatusAsync(long userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return false;
        }

        user.IsBlocked = !user.IsBlocked;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }

    public async Task<UserMaster?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<UserMaster?> GetUserByIdAsync(long id, CancellationToken cancellationToken = default)
        => await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<UserMaster?> GetUserByNameAsync(string name, CancellationToken cancellationToken = default)
        => await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == name);

    public async Task<UserMaster?> GetUserByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
        => await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

    public async Task<UserMaster?> GetUserByTokenAsync(string token, CancellationToken cancellationToken = default)
        => await _userManager.Users.Include(x => x.RefreshTokens).SingleOrDefaultAsync(x => x.RefreshTokens.Any(t => t.Token == token), cancellationToken);

    public async Task<string> GenerateEmailConfirmationTokenAsync(UserMaster user)
        => await _userManager.GenerateEmailConfirmationTokenAsync(user);







    public async Task<bool> ManageRefreshTokensAsync(long userId, RefreshToken newRefreshToken, CancellationToken cancellationToken = default)
    {
        // Get the tracked user entity with refresh tokens
        var user = await _userManager.Users
            .AsTracking()
            .Include(x => x.RefreshTokens)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (user == null)
        {
            return false;
        }

        var currentTime = DateTimeProvider.UtcNow;

        // Revoke all old active tokens
        foreach (var token in user.RefreshTokens.Where(t => t.IsActive))
        {
            token.RevokedAt = currentTime;
        }

        // Delete old tokens if count >= 10
        if (user.RefreshTokens.Count >= 10)
        {
            var tokensToDelete = user.RefreshTokens
                .OrderBy(t => t.Id)
                .Take(5)
                .ToList();

            foreach (var token in tokensToDelete)
            {
                user.RefreshTokens.Remove(token);
            }
        }

        // Add new token
        user.RefreshTokens.Add(newRefreshToken);

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> RevokeRefreshTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users
            .AsTracking()
            .Include(x => x.RefreshTokens)
            .FirstOrDefaultAsync(x => x.RefreshTokens.Any(t => t.Token == token), cancellationToken);

        if (user == null)
        {
            return false;
        }

        var refreshToken = user.RefreshTokens.FirstOrDefault(t => t.Token == token);
        if (refreshToken == null || !refreshToken.IsActive)
        {
            return false;
        }

        refreshToken.RevokedAt = DateTimeProvider.UtcNow;

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }








    private async Task<bool> ConfigureAndCreateUser(UserMaster user, string password, CancellationToken cancellationToken = default)
    {
        user.PasswordHash = PasswordGeneratorHelper.HashPassword(user, password);

        user.NormalizedEmail = user.Email?.ToUpper();

        user.NormalizedUserName = user.UserName?.ToUpper();

        user.SecurityStamp = Guid.NewGuid().ToString();

        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(result.Errors.FirstOrDefault()?.Description);
        }

        return result.Succeeded;
    }

}
