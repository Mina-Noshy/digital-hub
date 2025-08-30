using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Auth;
using DigitalHub.Application.Services.Auth.Common;
using DigitalHub.Domain.Constants;
using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using DigitalHub.Domain.Utilities;

using UserLoginHistoryMasterEntity = DigitalHub.Domain.Entities.Auth.UserLoginHistoryMaster;

namespace DigitalHub.Application.Services.Auth.Auth;

public record GetTokenQuery
    (GetTokenDto requestDto, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetTokenQueryHandler(IAuthRepository _repository,
    IAccountRepository _accountRepository,
    IRoleMasterRepository _roleMasterRepository,
    ICurrentUser _currentUser,
    IPathFinderService _pathFinderService,
    IUserLoginHistoryMasterRepository _loginHistoryMasterRepository) : IQueryHandler<GetTokenQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await TryGetUserAsync(request.requestDto.UserName, cancellationToken);

        if (user == null)
        {
            return ApiResponse.Unauthorized(ApiMessage.InvalidCredentials);
        }

        var confirmationMessage = AuthHelper.GetConfirmationMessage(user, request.requestDto.UserName);
        if (confirmationMessage != null)
        {
            return ApiResponse.Unauthorized(confirmationMessage);
        }
        else if (!user.IsActive)
        {
            return ApiResponse.Unauthorized(ApiMessage.AccountInactive);
        }
        else if (user.IsBlocked)
        {
            return ApiResponse.Unauthorized(ApiMessage.AccountBlocked);
        }

        var isAuthorized = await _repository.CheckPasswordAsync(user, request.requestDto.Password, cancellationToken);
        if (!isAuthorized)
        {
            return ApiResponse.Unauthorized(ApiMessage.InvalidCredentials);
        }

        await AddUserLoginHistory(user.Id, cancellationToken);

        var userInfo = await ConfigureAndGetUserInfo(user, cancellationToken);
        return ApiResponse.Success(data: userInfo);
    }

    private async Task<bool> AddUserLoginHistory(long userId, CancellationToken cancellationToken)
    {
        var entity = new UserLoginHistoryMasterEntity()
        {
            UserId = userId,
            IpAddress = _currentUser.IpAddress,
            UserAgent = _currentUser.UserAgent,
            LoginDate = DateTimeProvider.UtcNow,
            LoginType = "Local"
        };

        return await _loginHistoryMasterRepository.AddAsync(entity, cancellationToken);
    }

    private async Task<UserMaster?> TryGetUserAsync(string userName, CancellationToken cancellationToken)
    {
        var user =
            await _accountRepository.GetUserByEmailAsync(userName, cancellationToken)
         ?? await _accountRepository.GetUserByNameAsync(userName, cancellationToken)
         ?? await _accountRepository.GetUserByPhoneNumberAsync(userName, cancellationToken);

        return user;
    }

    private async Task<UserInfoDto> ConfigureAndGetUserInfo(UserMaster user, CancellationToken cancellationToken = default)
    {
        var roles = (await _roleMasterRepository.GetUserRolesAsync(user, cancellationToken))?.ToList();

        var userInfoDto = AuthHelper.ConfigureAndGetUserInfo(user, DateTimeProvider.UtcNow, _pathFinderService, roles);

        var refreshToken = new RefreshToken()
        {
            Token = userInfoDto.RefreshToken,
            ExpiresAt = DateTimeProvider.UtcNow.AddDays(int.Parse(ConfigurationHelper.GetJWT(JWTKeys.RefreshTokenExpirationInDays))),
            CreatedAt = DateTimeProvider.UtcNow
        };

        // Handle refresh token management using the repository method
        await _accountRepository.ManageRefreshTokensAsync(user.Id, refreshToken, cancellationToken);

        return userInfoDto;
    }
}

