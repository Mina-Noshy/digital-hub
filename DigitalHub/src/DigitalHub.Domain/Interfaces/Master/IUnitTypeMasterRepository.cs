using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IUnitTypeMasterRepository : IBaseRepository
{
    IQueryable<UnitTypeMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<UnitTypeMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(UnitTypeMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UnitTypeMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(UnitTypeMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<UnitTypeMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
