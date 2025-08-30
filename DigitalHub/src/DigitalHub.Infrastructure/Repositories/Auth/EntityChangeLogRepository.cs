using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace DigitalHub.Infrastructure.Repositories.Auth;

internal class EntityChangeLogRepository(IUnitOfWork _unitOfWork) : IEntityChangeLogRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public async Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository(true).CountAsync<EntityChangeLog>(
            GetFilterExpression(queryParams),
            cancellationToken);
    }

    public async Task<IEnumerable<EntityChangeLog>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        var orderByExpression = queryParams.Ascending ? queryParams.SortColumn : $"{queryParams.SortColumn} DESC";

        return await _unitOfWork.Repository(true).FindAsync<EntityChangeLog>(
            GetFilterExpression(queryParams),
            string.IsNullOrWhiteSpace(queryParams.SortColumn) ? null : o => o.OrderBy(orderByExpression),
            queryParams.PageNumber,
            queryParams.PageSize,
            cancellationToken);
    }


    private Expression<Func<EntityChangeLog, bool>> GetFilterExpression(PaginationQueryParams queryParams)
    {
        // Start with a base expression (always true)
        Expression<Func<EntityChangeLog, bool>> baseFilter = x =>
            string.IsNullOrWhiteSpace(queryParams.SearchTerm) || (x.ChangedBy + x.Operation).Contains(queryParams.SearchTerm);

        if (queryParams.FromDate.HasValue)
        {
            Expression<Func<EntityChangeLog, bool>> filter1 = x => x.Timestamp >= queryParams.FromDate.Value;
            baseFilter = baseFilter.And(filter1);
        }
        if (queryParams.ToDate.HasValue)
        {
            Expression<Func<EntityChangeLog, bool>> filter1 = x => x.Timestamp <= queryParams.ToDate.Value;
            baseFilter = baseFilter.And(filter1);
        }
        return baseFilter;
    }
}
