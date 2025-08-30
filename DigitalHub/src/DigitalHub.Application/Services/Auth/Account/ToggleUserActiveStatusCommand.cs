using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Domain.Interfaces.Auth;

namespace DigitalHub.Application.Services.Auth.Account;

public record ToggleUserActiveStatusCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class ToggleUserActiveStatusCommandHandler(IAccountRepository _repository) : ICommandHandler<ToggleUserActiveStatusCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(ToggleUserActiveStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.ToggleUserActiveStatusAsync(request.id, cancellationToken);
        if (result)
        {
            return ApiResponse.Success(ApiMessage.AccountActivationToggled);
        }
        return ApiResponse.Failure(ApiMessage.SomethingWrong);
    }
}
