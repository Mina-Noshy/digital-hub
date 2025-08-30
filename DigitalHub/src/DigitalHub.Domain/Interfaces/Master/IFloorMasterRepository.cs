using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IFloorMasterRepository : IBaseRepository
{
    IQueryable<FloorMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<FloorMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(FloorMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(FloorMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(FloorMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<FloorMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
