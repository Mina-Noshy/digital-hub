using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.Services.Auth.Common;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using Microsoft.AspNetCore.Http;

namespace DigitalHub.Application.Services.Auth.Account;

public record SendConfirmationEmailCommand
    (string email, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class SendConfirmationEmailCommandHandler(IAccountRepository _repository,
    IHttpContextAccessor _httpContextAccessor,
    ICompanyProfileSettingsRepository _companyProfileSettings,
    IEmailSender _emailSender) : ICommandHandler<SendConfirmationEmailCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(SendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByEmailAsync(request.email, cancellationToken);
        if (user == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var token = await _repository.GenerateEmailConfirmationTokenAsync(user);
        var companyProfile = await _companyProfileSettings.GetSettingsAsync(cancellationToken);

        if (!await AuthHelper.SendConfirmationEmailAsync(user, companyProfile!, _httpContextAccessor.HttpContext!.Request, _emailSender, token, cancellationToken))
        {
            return ApiResponse.Failure(ApiMessage.SomethingWrong);
        }

        return ApiResponse.Success(ApiMessage.EmailConfirmed);
    }
}
