using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IPriorityMasterRepository : IBaseRepository
{
    IQueryable<PriorityMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<PriorityMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(PriorityMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(PriorityMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(PriorityMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<PriorityMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
