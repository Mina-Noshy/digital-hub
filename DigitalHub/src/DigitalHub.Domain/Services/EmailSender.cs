
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;

namespace DigitalHub.Domain.Services;

public class EmailSender(ICompanyProfileSettingsRepository _settingsRepository, ICurrentUser _currentUser) : IEmailSender
{
    public EmailBuilder Compose()
    {
        return new EmailBuilder(_settingsRepository, _currentUser);
    }

}










