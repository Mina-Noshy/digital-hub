using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Domain.Interfaces.Auth;

namespace DigitalHub.Application.Services.Auth.Account;

public record ToggleUserBlockStatusCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class ToggleUserBlockStatusCommandHandler(IAccountRepository _repository) : ICommandHandler<ToggleUserBlockStatusCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(ToggleUserBlockStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.ToggleUserBlockStatusAsync(request.id, cancellationToken);
        if (result)
        {
            return ApiResponse.Success(ApiMessage.AccountBlockationToggled);
        }
        return ApiResponse.Failure(ApiMessage.SomethingWrong);
    }
}