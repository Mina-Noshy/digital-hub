using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IUnitStyleMasterRepository : IBaseRepository
{
    IQueryable<UnitStyleMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<UnitStyleMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(UnitStyleMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UnitStyleMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(UnitStyleMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<UnitStyleMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
