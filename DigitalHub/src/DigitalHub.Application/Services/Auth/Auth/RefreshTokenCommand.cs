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

namespace DigitalHub.Application.Services.Auth.Auth;

public record RefreshTokenCommand
    (RefreshTokenDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class RefreshTokenCommandHandler(
    IAccountRepository _accountRepository,
    IRoleMasterRepository _roleMasterRepository,
    IPathFinderService _pathFinderService) : ICommandHandler<RefreshTokenCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _accountRepository.GetUserByTokenAsync(request.requestDto.Token, cancellationToken);

        if (user == null)
        {
            return ApiResponse.Unauthorized(ApiMessage.TokenExpired);
        }

        if (!user.IsActive)
        {
            return ApiResponse.Unauthorized(ApiMessage.AccountInactive);
        }

        if (user.IsBlocked)
        {
            return ApiResponse.Unauthorized(ApiMessage.AccountBlocked);
        }

        var refreshToken = user.RefreshTokens.Single(t => t.Token == request.requestDto.Token);

        if (!refreshToken.IsActive)
        {
            return ApiResponse.Unauthorized(ApiMessage.TokenExpired);
        }

        var userInfo = await ConfigureAndGetUserInfo(user, refreshToken, cancellationToken);
        return ApiResponse.Success(data: userInfo);
    }












    private async Task<UserInfoDto> ConfigureAndGetUserInfo(UserMaster user, RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        var roles = (await _roleMasterRepository.GetUserRolesAsync(user, cancellationToken))?.ToList();

        var userInfoDto = AuthHelper.ConfigureAndGetUserInfo(user, DateTimeProvider.UtcNow, _pathFinderService, roles);

        // Refresh the token
        refreshToken.Token = userInfoDto.RefreshToken;
        refreshToken.CreatedAt = DateTimeProvider.UtcNow;
        refreshToken.ExpiresAt = DateTimeProvider.UtcNow.AddDays(int.Parse(ConfigurationHelper.GetJWT(JWTKeys.RefreshTokenExpirationInDays)));

        await _accountRepository.UpdateUserAsync(user, cancellationToken);

        return userInfoDto;
    }

}