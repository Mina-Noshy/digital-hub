using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;

namespace DigitalHub.Domain.Interfaces.Auth;

public interface ICompanyProfileSettingsRepository : IBaseRepository
{
    Task<CompanyProfileSettings?> GetSettingsAsync(CancellationToken cancellationToken);
    Task<bool> UpdateAsync(CompanyProfileSettings entity, CancellationToken cancellationToken);
}