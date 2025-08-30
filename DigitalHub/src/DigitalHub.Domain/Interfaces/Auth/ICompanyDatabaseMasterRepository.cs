using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;

namespace DigitalHub.Domain.Interfaces.Auth;

public interface ICompanyDatabaseMasterRepository : IBaseRepository
{
    Task<CompanyDatabaseMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<CompanyDatabaseMaster?> GetByDatabaseNoAsync(string databaseNo, CancellationToken cancellationToken);

    Task<IEnumerable<CompanyDatabaseMaster>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<CompanyDatabaseMaster>> GetAllActiveAsync(CancellationToken cancellationToken);
    Task<IEnumerable<CompanyDatabaseMaster>> GetAllForClientAsync(string companyKey, CancellationToken cancellationToken);
}
