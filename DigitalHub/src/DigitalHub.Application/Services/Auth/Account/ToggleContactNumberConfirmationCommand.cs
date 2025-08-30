using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Domain.Interfaces.Auth;

namespace DigitalHub.Application.Services.Auth.Account;

public record ToggleContactNumberConfirmationCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class ToggleContactNumberConfirmationCommandHandler(IAccountRepository _repository) : ICommandHandler<ToggleContactNumberConfirmationCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(ToggleContactNumberConfirmationCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.ToggleContactNumberConfirmationAsync(request.id, cancellationToken);
        if (result)
        {
            return ApiResponse.Success(ApiMessage.ContactNumberConfirmationToggled);
        }
        return ApiResponse.Failure(ApiMessage.SomethingWrong);
    }
}