using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Domain.Interfaces.Auth;

namespace DigitalHub.Application.Services.Auth.Account;

public record ToggleEmailConfirmationCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class ToggleEmailConfirmationCommandHandler(IAccountRepository _repository) : ICommandHandler<ToggleEmailConfirmationCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(ToggleEmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.ToggleEmailConfirmationAsync(request.id, cancellationToken);
        if (result)
        {
            return ApiResponse.Success(ApiMessage.EmailConfirmationToggled);
        }
        return ApiResponse.Failure(ApiMessage.SomethingWrong);
    }
}