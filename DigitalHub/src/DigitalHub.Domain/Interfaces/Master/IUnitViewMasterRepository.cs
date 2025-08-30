using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IUnitViewMasterRepository : IBaseRepository
{
    IQueryable<UnitViewMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<UnitViewMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(UnitViewMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UnitViewMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(UnitViewMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<UnitViewMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
