using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace DigitalHub.Infrastructure.Repositories.Auth;

internal class MenuMasterRepository(IUnitOfWork _unitOfWork) : IMenuMasterRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public IQueryable<MenuMaster> GetAsDropdown(DropdownQueryParams queryParams)
    {
        return _unitOfWork.Repository().GetLightCollection<MenuMaster>(
            GetFilterExpression(queryParams),
            o => o.OrderBy(x => x.Name),
            1,
            queryParams.Limit);
    }
    public async Task<MenuMaster?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().GetByIdAsync<MenuMaster>(id, cancellationToken, nameof(MenuMaster.GetModule));
    }
    public async Task<bool> AddAsync(MenuMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Add(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> UpdateAsync(MenuMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Update(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> DeleteAsync(MenuMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().SoftDelete(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository().CountAsync<MenuMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm),
            cancellationToken);
    }
    public IQueryable<MenuMaster> GetPaged(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        var orderByExpression = queryParams.Ascending ? queryParams.SortColumn : $"{queryParams.SortColumn} DESC";

        return _unitOfWork.Repository().Find<MenuMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm),
            string.IsNullOrWhiteSpace(queryParams.SortColumn) ? null : o => o.OrderBy(orderByExpression),
            queryParams.PageNumber,
            queryParams.PageSize,
            nameof(MenuMaster.GetModule));
    }

    private Expression<Func<MenuMaster, bool>> GetFilterExpression(DropdownQueryParams queryParams)
    {
        // Start with a base expression (always true)
        Expression<Func<MenuMaster, bool>> baseFilter = x =>
            string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm);

        if (queryParams.ParentId > 0)
        {
            Expression<Func<MenuMaster, bool>> filter1 = x => x.ModuleId == queryParams.ParentId;
            baseFilter = baseFilter.And(filter1);
        }

        return baseFilter;
    }

}
