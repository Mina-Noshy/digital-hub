using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace DigitalHub.Infrastructure.Repositories.Master;

internal class TermsAndConditionsMasterRepository(IUnitOfWork _unitOfWork) : ITermsAndConditionsMasterRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public IQueryable<TermsAndConditionsMaster> GetAsDropdown(DropdownQueryParams queryParams)
    {
        return _unitOfWork.Repository().GetLightCollection<TermsAndConditionsMaster>(
            GetFilterExpression(queryParams),
            o => o.OrderBy(x => x.Description),
            1,
            queryParams.Limit);
    }
    public async Task<TermsAndConditionsMaster?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().GetByIdAsync<TermsAndConditionsMaster>(id, cancellationToken, nameof(TermsAndConditionsMaster.GetCategory));
    }
    public async Task<bool> AddAsync(TermsAndConditionsMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Add(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> UpdateAsync(TermsAndConditionsMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Update(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> DeleteAsync(TermsAndConditionsMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().SoftDelete(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository().CountAsync<TermsAndConditionsMaster>(
            GetFilterExpression(queryParams),
            cancellationToken);
    }
    public IQueryable<TermsAndConditionsMaster> GetPaged(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        var orderByExpression = queryParams.Ascending ? queryParams.SortColumn : $"{queryParams.SortColumn} DESC";

        return _unitOfWork.Repository().Find<TermsAndConditionsMaster>(
            GetFilterExpression(queryParams),
            string.IsNullOrWhiteSpace(queryParams.SortColumn) ? null : o => o.OrderBy(orderByExpression),
            queryParams.PageNumber,
            queryParams.PageSize,
            nameof(TermsAndConditionsMaster.GetCategory));
    }















    private Expression<Func<TermsAndConditionsMaster, bool>> GetFilterExpression(PaginationQueryParams queryParams)
    {
        // Start with a base expression (always true)
        Expression<Func<TermsAndConditionsMaster, bool>> baseFilter = x =>
            string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Description.Contains(queryParams.SearchTerm);

        if (queryParams.ParentId > 0)
        {
            Expression<Func<TermsAndConditionsMaster, bool>> filter1 = x => x.CategoryId == queryParams.ParentId;
            baseFilter = baseFilter.And(filter1);
        }

        return baseFilter;
    }

    private Expression<Func<TermsAndConditionsMaster, bool>> GetFilterExpression(DropdownQueryParams queryParams)
    {
        // Start with a base expression (always true)
        Expression<Func<TermsAndConditionsMaster, bool>> baseFilter = x =>
            string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Description.Contains(queryParams.SearchTerm);

        if (queryParams.ParentId > 0)
        {
            Expression<Func<TermsAndConditionsMaster, bool>> filter1 = x => x.CategoryId == queryParams.ParentId;
            baseFilter = baseFilter.And(filter1);
        }

        return baseFilter;
    }
}
