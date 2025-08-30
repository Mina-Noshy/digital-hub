using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;

namespace DigitalHub.Infrastructure.Repositories.Auth;


internal class CompanyProfileSettingsRepository(IUnitOfWork _unitOfWork) : ICompanyProfileSettingsRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public async Task<CompanyProfileSettings?> GetSettingsAsync(CancellationToken cancellationToken)
    {
        var tableName = nameof(CompanyProfileSettings);
        string sql = $"SELECT TOP (1) * FROM [{tableName}]";

        return
            await _unitOfWork.Repository().QueryFirstOrDefaultAsync<CompanyProfileSettings>(sql, null, cancellationToken);
    }
    public async Task<bool> UpdateAsync(CompanyProfileSettings entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Update(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
}
