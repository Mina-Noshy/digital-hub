using DigitalHub.Domain.Utilities;

namespace DigitalHub.Application.Services.Common;

// Changed 'private' to 'internal' to fix CS1527 error
internal class UniqueNumberGenerator
{
    internal string Generate(string prefix) =>
        $"{prefix}-{DateTimeProvider.UtcNow:yy-MMdd-HHmm-ssff}";

}
