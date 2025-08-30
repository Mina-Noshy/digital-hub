using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using System.Linq.Dynamic.Core;

namespace DigitalHub.Infrastructure.Repositories.Master;

internal class NationalityMasterRepository(IUnitOfWork _unitOfWork) : INationalityMasterRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public IQueryable<NationalityMaster> GetAsDropdown(DropdownQueryParams queryParams)
    {
        return _unitOfWork.Repository().GetLightCollection<NationalityMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm),
            o => o.OrderBy(x => x.Name),
            1,
            queryParams.Limit);
    }
    public async Task<NationalityMaster?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().GetByIdAsync<NationalityMaster>(id, cancellationToken);
    }
    public async Task<bool> AddAsync(NationalityMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Add(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> UpdateAsync(NationalityMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Update(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> DeleteAsync(NationalityMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().SoftDelete(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository().CountAsync<NationalityMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm),
            cancellationToken);
    }
    public async Task<IEnumerable<NationalityMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        var orderByExpression = queryParams.Ascending ? queryParams.SortColumn : $"{queryParams.SortColumn} DESC";

        return await _unitOfWork.Repository().FindAsync<NationalityMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm),
            string.IsNullOrWhiteSpace(queryParams.SortColumn) ? null : o => o.OrderBy(orderByExpression),
            queryParams.PageNumber,
            queryParams.PageSize,
            cancellationToken);
    }
}
