using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Account;
using DigitalHub.Domain.Interfaces.Auth;

namespace DigitalHub.Application.Services.Auth.Account;

public record ConfirmEmailCommand
    (ConfirmEmailDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class ConfirmEmailCommandHandler(IAccountRepository _repository) : ICommandHandler<ConfirmEmailCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.ConfirmEmailAsync(request.requestDto.UserId, request.requestDto.Token, cancellationToken);
        if (result)
        {
            return ApiResponse.Success(ApiMessage.EmailConfirmed);
        }

        return ApiResponse.Failure(ApiMessage.SomethingWrong);
    }
}