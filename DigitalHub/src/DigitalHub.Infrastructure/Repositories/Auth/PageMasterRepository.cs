using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace DigitalHub.Infrastructure.Repositories.Auth;

internal class PageMasterRepository(IUnitOfWork _unitOfWork) : IPageMasterRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public IQueryable<PageMaster> GetAsDropdown(DropdownQueryParams queryParams)
    {
        return _unitOfWork.Repository().GetLightCollection<PageMaster>(
            GetFilterExpression(queryParams),
            o => o.OrderBy(x => x.Name),
            1,
            queryParams.Limit);
    }
    public async Task<PageMaster?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().GetByIdAsync<PageMaster>(
                id,
                cancellationToken,
                nameof(PageMaster.GetMenu) + "." + nameof(PageMaster.GetMenu.GetModule));
    }
    public async Task<bool> AddAsync(PageMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Add(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> UpdateAsync(PageMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Update(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> DeleteAsync(PageMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().SoftDelete(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository().CountAsync<PageMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm),
            cancellationToken);
    }
    public IQueryable<PageMaster> GetPaged(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        var orderByExpression = queryParams.Ascending ? queryParams.SortColumn : $"{queryParams.SortColumn} DESC";

        return _unitOfWork.Repository().Find<PageMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm),
            string.IsNullOrWhiteSpace(queryParams.SortColumn) ? null : o => o.OrderBy(orderByExpression),
            queryParams.PageNumber,
            queryParams.PageSize,
            nameof(PageMaster.GetMenu) + "." + nameof(PageMaster.GetMenu.GetModule));
    }

    private Expression<Func<PageMaster, bool>> GetFilterExpression(DropdownQueryParams queryParams)
    {
        // Start with a base expression (always true)
        Expression<Func<PageMaster, bool>> baseFilter = x =>
            string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm);

        if (queryParams.ParentId > 0)
        {
            Expression<Func<PageMaster, bool>> filter1 = x => x.MenuId == queryParams.ParentId;
            baseFilter = baseFilter.And(filter1);
        }

        return baseFilter;
    }

}
