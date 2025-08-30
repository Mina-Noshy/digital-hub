using DigitalHub.Domain.Entities.HR;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.HR;

public interface IEmploymentTypeMasterRepository : IBaseRepository
{
    IQueryable<EmploymentTypeMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<EmploymentTypeMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(EmploymentTypeMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(EmploymentTypeMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(EmploymentTypeMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<EmploymentTypeMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
