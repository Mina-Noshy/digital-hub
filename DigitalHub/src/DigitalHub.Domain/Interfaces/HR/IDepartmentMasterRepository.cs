using DigitalHub.Domain.Entities.HR;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.HR;

public interface IDepartmentMasterRepository : IBaseRepository
{
    IQueryable<DepartmentMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<DepartmentMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(DepartmentMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(DepartmentMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(DepartmentMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<DepartmentMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
