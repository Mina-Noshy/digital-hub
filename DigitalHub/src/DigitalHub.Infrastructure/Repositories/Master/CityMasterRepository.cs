using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace DigitalHub.Infrastructure.Repositories.Master;

internal class CityMasterRepository(IUnitOfWork _unitOfWork) : ICityMasterRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public IQueryable<CityMaster> GetAsDropdown(DropdownQueryParams queryParams)
    {
        return _unitOfWork.Repository().GetLightCollection(
            GetFilterExpression(queryParams),
            o => o.OrderBy(x => x.Name),
            1,
            queryParams.Limit);
    }
    public async Task<CityMaster?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().GetByIdAsync<CityMaster>(id, cancellationToken, nameof(CityMaster.GetCountry));
    }
    public async Task<bool> AddAsync(CityMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Add(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> UpdateAsync(CityMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Update(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> DeleteAsync(CityMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().SoftDelete(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository().CountAsync<CityMaster>(
            GetFilterExpression(queryParams),
            cancellationToken);
    }
    public IQueryable<CityMaster> GetPaged(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        var orderByExpression = queryParams.Ascending ? queryParams.SortColumn : $"{queryParams.SortColumn} DESC";

        return _unitOfWork.Repository().Find<CityMaster>(
            GetFilterExpression(queryParams),
            string.IsNullOrWhiteSpace(queryParams.SortColumn) ? null : o => o.OrderBy(orderByExpression),
            queryParams.PageNumber,
            queryParams.PageSize,
            nameof(CityMaster.GetCountry));
    }







    private Expression<Func<CityMaster, bool>> GetFilterExpression(PaginationQueryParams queryParams)
    {
        // Start with a base expression (always true)
        Expression<Func<CityMaster, bool>> baseFilter = x =>
            string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm);

        if (queryParams.ParentId > 0)
        {
            Expression<Func<CityMaster, bool>> filter1 = x => x.CountryId == queryParams.ParentId;
            baseFilter = baseFilter.And(filter1);
        }

        return baseFilter;
    }

    private Expression<Func<CityMaster, bool>> GetFilterExpression(DropdownQueryParams queryParams)
    {
        // Start with a base expression (always true)
        Expression<Func<CityMaster, bool>> baseFilter = x =>
            string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm);

        if (queryParams.ParentId > 0)
        {
            Expression<Func<CityMaster, bool>> filter1 = x => x.CountryId == queryParams.ParentId;
            baseFilter = baseFilter.And(filter1);
        }

        return baseFilter;
    }

}
