using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IUnitModelMasterRepository : IBaseRepository
{
    IQueryable<UnitModelMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<UnitModelMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(UnitModelMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UnitModelMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(UnitModelMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<UnitModelMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
