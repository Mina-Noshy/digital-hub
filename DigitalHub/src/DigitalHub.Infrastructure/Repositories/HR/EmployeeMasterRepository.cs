using DigitalHub.Domain.Entities.HR;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.Interfaces.HR;
using DigitalHub.Domain.QueryParams.Common;
using DigitalHub.Domain.QueryParams.HR;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace DigitalHub.Infrastructure.Repositories.HR;

internal class EmployeeMasterRepository(IUnitOfWork _unitOfWork) : IEmployeeMasterRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public IQueryable<EmployeeMaster> GetAsDropdown(DropdownQueryParams queryParams)
    {
        return _unitOfWork.Repository().GetLightCollection<EmployeeMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || (x.Title + " " + x.FirstName + " " + x.LastName).Contains(queryParams.SearchTerm),
            o => o.OrderBy(x => x.Title).ThenBy(x => x.FirstName).ThenBy(x => x.LastName),
            1,
            queryParams.Limit);
    }
    public async Task<string> GetNextEmployeeCodeAsync(CancellationToken cancellationToken)
    {
        var maxId = await _unitOfWork.Repository().MaxAsync<EmployeeMaster, long?>(x => true, s => s.Id, cancellationToken);
        var nextId = (maxId ?? 0) + 1;
        return $"EC-{nextId:D5}";
    }
    public async Task<EmployeeMaster?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().GetByIdAsync<EmployeeMaster>(
                id,
                cancellationToken,
                nameof(EmployeeMaster.GetDepartment),
                nameof(EmployeeMaster.GetEmployeeRole),
                nameof(EmployeeMaster.GetEmploymentType));
    }
    public async Task<EmployeeMaster?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().FirstOrDefaultAsync<EmployeeMaster>(
                x => x.Email == email,
                cancellationToken,
                nameof(EmployeeMaster.GetDepartment),
                nameof(EmployeeMaster.GetEmployeeRole),
                nameof(EmployeeMaster.GetEmploymentType));
    }
    public async Task<EmployeeMaster?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().FirstOrDefaultAsync<EmployeeMaster>(
                x => x.PhoneNumber == phoneNumber,
                cancellationToken,
                nameof(EmployeeMaster.GetDepartment),
                nameof(EmployeeMaster.GetEmployeeRole),
                nameof(EmployeeMaster.GetEmploymentType));
    }
    public async Task<bool> AddAsync(EmployeeMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Add(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> UpdateAsync(EmployeeMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Update(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> DeleteAsync(EmployeeMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().SoftDelete(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<int> CountAsync(EmployeeMasterQueryParams queryParams, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository().CountAsync<EmployeeMaster>(
            GetFilterExpression(queryParams),
            cancellationToken);
    }
    public async Task<IEnumerable<EmployeeMaster>> GetPagedAsync(EmployeeMasterQueryParams queryParams, CancellationToken cancellationToken)
    {
        var orderByExpression = queryParams.Ascending ? queryParams.SortColumn : $"{queryParams.SortColumn} DESC";

        return await _unitOfWork.Repository().FindAsync<EmployeeMaster>(
            GetFilterExpression(queryParams),
            string.IsNullOrWhiteSpace(queryParams.SortColumn) ? null : o => o.OrderBy(orderByExpression),
            queryParams.PageNumber,
            queryParams.PageSize,
            cancellationToken,
            nameof(EmployeeMaster.GetDepartment),
            nameof(EmployeeMaster.GetEmployeeRole),
            nameof(EmployeeMaster.GetEmploymentType));
    }



    private Expression<Func<EmployeeMaster, bool>> GetFilterExpression(EmployeeMasterQueryParams queryParams)
    {
        // Start with a base expression (always true)
        Expression<Func<EmployeeMaster, bool>> baseFilter = x =>
            string.IsNullOrWhiteSpace(queryParams.SearchTerm) || (x.Title + " " + x.FirstName + " " + x.LastName).Contains(queryParams.SearchTerm);

        if (queryParams.DepartmentId > 0)
        {
            Expression<Func<EmployeeMaster, bool>> filter1 = x => x.DepartmentId == queryParams.DepartmentId;
            baseFilter = baseFilter.And(filter1);
        }

        if (queryParams.RoleId > 0)
        {
            Expression<Func<EmployeeMaster, bool>> filter1 = x => x.RoleId == queryParams.RoleId;
            baseFilter = baseFilter.And(filter1);
        }

        if (queryParams.EmploymentTypeId > 0)
        {
            Expression<Func<EmployeeMaster, bool>> filter1 = x => x.EmploymentTypeId == queryParams.EmploymentTypeId;
            baseFilter = baseFilter.And(filter1);
        }

        if (queryParams.FromDate.HasValue)
        {
            Expression<Func<EmployeeMaster, bool>> filter1 = x => x.HireDate.Date >= queryParams.FromDate.Value.Date;
            baseFilter = baseFilter.And(filter1);
        }

        if (queryParams.ToDate.HasValue)
        {
            Expression<Func<EmployeeMaster, bool>> filter1 = x => x.HireDate.Date <= queryParams.ToDate.Value.Date;
            baseFilter = baseFilter.And(filter1);
        }

        return baseFilter;
    }


}
