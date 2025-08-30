using DigitalHub.Domain.Services;

namespace DigitalHub.Domain.Interfaces.Common;

public interface IEmailSender
{
    EmailBuilder Compose();
}
