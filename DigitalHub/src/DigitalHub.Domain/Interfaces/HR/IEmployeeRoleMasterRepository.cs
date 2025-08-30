using DigitalHub.Domain.Entities.HR;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.HR;

public interface IEmployeeRoleMasterRepository : IBaseRepository
{
    IQueryable<EmployeeRoleMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<EmployeeRoleMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(EmployeeRoleMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(EmployeeRoleMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(EmployeeRoleMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<EmployeeRoleMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
