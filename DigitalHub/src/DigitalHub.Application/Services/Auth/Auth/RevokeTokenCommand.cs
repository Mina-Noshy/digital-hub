using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Auth;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Utilities;

namespace DigitalHub.Application.Services.Auth.Auth;

public record RevokeTokenCommand
    (TokenDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class RevokeTokenCommandHandler(IAccountRepository _accountRepository) : ICommandHandler<RevokeTokenCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _accountRepository.GetUserByTokenAsync(request.requestDto.Token, cancellationToken);
        if (user is null)
        {
            return ApiResponse.Failure(ApiMessage.TokenExpired);
        }

        var refreshToken = user.RefreshTokens.Single(t => t.Token == request.requestDto.Token);
        if (refreshToken.IsActive == false)
        {
            return ApiResponse.Failure(ApiMessage.TokenExpired);
        }

        refreshToken.RevokedAt = DateTimeProvider.UtcNow;
        await _accountRepository.UpdateUserAsync(user);

        return ApiResponse.Success(ApiMessage.SuccessfulDelete);
    }
}