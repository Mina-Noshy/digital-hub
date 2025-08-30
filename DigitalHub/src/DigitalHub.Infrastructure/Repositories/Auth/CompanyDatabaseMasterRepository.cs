using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;

namespace DigitalHub.Infrastructure.Repositories.Auth;

public class CompanyDatabaseMasterRepository(IUnitOfWork _unitOfWork) : ICompanyDatabaseMasterRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public async Task<IEnumerable<CompanyDatabaseMaster>> GetAllActiveAsync(CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().GetAllAsync<CompanyDatabaseMaster>(x =>
            x.IsActive, o => o.OrderBy(s => s.CompanyName), cancellationToken);
    }

    public async Task<IEnumerable<CompanyDatabaseMaster>> GetAllAsync(CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().GetAllAsync<CompanyDatabaseMaster>(x =>
            true, o => o.OrderBy(s => s.CompanyName), cancellationToken);
    }

    public async Task<IEnumerable<CompanyDatabaseMaster>> GetAllForClientAsync(string companyKey, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().GetAllAsync<CompanyDatabaseMaster>(x =>
            x.IsActive && x.CompanyKey == companyKey, o => o.OrderBy(s => s.CompanyName), cancellationToken);
    }

    public async Task<CompanyDatabaseMaster?> GetByDatabaseNoAsync(string databaseNo, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().FirstOrDefaultAsync<CompanyDatabaseMaster>(x => x.DatabaseNo == databaseNo, cancellationToken);
    }

    public async Task<CompanyDatabaseMaster?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().GetByIdAsync<CompanyDatabaseMaster>(id, cancellationToken);
    }
}
