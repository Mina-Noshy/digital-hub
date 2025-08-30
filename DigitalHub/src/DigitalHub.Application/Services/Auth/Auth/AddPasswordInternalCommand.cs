using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Domain.Constants;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using DigitalHub.Domain.Utilities;

namespace DigitalHub.Application.Services.Auth.Auth;

public record AddPasswordInternalCommand
    (CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class AddPasswordInternalCommandHandler(
    IAccountRepository _accountRepository,
    ICurrentUser _currentUser,
    IEmailSender _emailSender,
    ICompanyProfileSettingsRepository _companyProfileSettingsRepository) : ICommandHandler<AddPasswordInternalCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(AddPasswordInternalCommand request, CancellationToken cancellationToken)
    {
        long userId = _currentUser.UserId;

        var user = await _accountRepository.GetUserByIdAsync(userId, cancellationToken);
        if (user == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        if (string.IsNullOrWhiteSpace(user.Email) || !user.EmailConfirmed)
        {
            return ApiResponse.Failure(ApiMessage.ConfirmationRequired);
        }

        string newPassword = PasswordGeneratorHelper.GeneratePassword(8, 4, 0, 4, 0);
        string encryptedPassword = PasswordGeneratorHelper.HashPassword(user, newPassword);
        user.PasswordHash = encryptedPassword;

        var result = await _accountRepository.UpdateUserAsync(user, cancellationToken);
        if (!result)
        {
            return ApiResponse.Failure(ApiMessage.FailedUpdate);
        }

        await SendUpdatedPasswordEmailNotificationAsync(user.FullName, user.Email, newPassword, cancellationToken);

        return ApiResponse.Success(ApiMessage.PasswordUpdated);
    }


    private async Task<bool> SendUpdatedPasswordEmailNotificationAsync(string userFullName, string email, string newPassword, CancellationToken cancellationToken)
    {
        var company = await _companyProfileSettingsRepository.GetSettingsAsync(cancellationToken);
        if (company == null)
        {
            return false;
        }

        var companyName = company.CompanyName;
        var subject =
            ConfigurationHelper.GetLocalizedEmailString(EmailTemplateKeys.PasswordUpdatedSubject)
            .Replace("{{company}}", companyName);

        string emailBody = ConfigurationHelper.GetLocalizedEmailString(EmailTemplateKeys.PasswordUpdatedBody);

        string body = emailBody
            .Replace("{{company}}", companyName)
            .Replace("{{name}}", userFullName)
            .Replace("{{password}}", newPassword);

        var isSent = await _emailSender
            .Compose()
            .To(email)
            .Subject(subject)
            .Body(body, true)
            .SendAsync(cancellationToken);

        return isSent;
    }
}