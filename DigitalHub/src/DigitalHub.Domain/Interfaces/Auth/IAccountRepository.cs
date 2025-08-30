using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Auth;

public interface IAccountRepository : IBaseRepository
{
    IQueryable<UserMaster> GetUsersAsDropdown(DropdownQueryParams queryParams);
    Task<IEnumerable<UserMaster>> GetUsersPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken = default);
    Task<int> GetUsersCountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken = default);
    Task<UserMaster?> GetUserByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<UserMaster?> GetUserByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<UserMaster?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<UserMaster?> GetUserByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<UserMaster?> GetUserByTokenAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> CreateUserAsync(UserMaster user, string password, CancellationToken cancellationToken = default);
    Task<bool> UpdateUserAsync(UserMaster user, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserAsync(UserMaster user, CancellationToken cancellationToken = default);
    Task<bool> ConfirmEmailAsync(long userId, string token, CancellationToken cancellationToken = default);
    Task<string> GenerateEmailConfirmationTokenAsync(UserMaster user);


    Task<bool> ManageRefreshTokensAsync(long userId, RefreshToken newRefreshToken, CancellationToken cancellationToken = default);
    Task<bool> RevokeRefreshTokenAsync(string token, CancellationToken cancellationToken = default);


    Task<bool> ToggleEmailConfirmationAsync(long userId, CancellationToken cancellationToken = default);
    Task<bool> ToggleContactNumberConfirmationAsync(long userId, CancellationToken cancellationToken = default);
    Task<bool> ToggleUserActiveStatusAsync(long userId, CancellationToken cancellationToken = default);
    Task<bool> ToggleUserBlockStatusAsync(long userId, CancellationToken cancellationToken = default);


}
