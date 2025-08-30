using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Auth;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;

namespace DigitalHub.Application.Services.Auth.Auth;

public record ChangePasswordCommand
    (ChangePasswordDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class ChangePasswordCommandHandler(
    IAuthRepository _repository,
    IAccountRepository _accountRepository,
    ICurrentUser _currentUser) : ICommandHandler<ChangePasswordCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        long userId = _currentUser.UserId;

        var user = await _accountRepository.GetUserByIdAsync(userId, cancellationToken);
        if (user == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var result = await _repository.ChangePasswordAsync(user, request.requestDto.CurrentPassword, request.requestDto.NewPassword, cancellationToken);
        if (result.Succeeded == false)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            var errorMessage = string.Join(", ", errors);

            return ApiResponse.Failure(errorMessage);
        }

        return ApiResponse.Success(ApiMessage.PasswordUpdated);
    }
}