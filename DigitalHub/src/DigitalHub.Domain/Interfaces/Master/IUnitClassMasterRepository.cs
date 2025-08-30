using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IUnitClassMasterRepository : IBaseRepository
{
    IQueryable<UnitClassMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<UnitClassMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(UnitClassMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UnitClassMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(UnitClassMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<UnitClassMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
