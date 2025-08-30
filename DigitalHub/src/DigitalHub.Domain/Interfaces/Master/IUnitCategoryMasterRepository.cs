using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IUnitCategoryMasterRepository : IBaseRepository
{
    IQueryable<UnitCategoryMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<UnitCategoryMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(UnitCategoryMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UnitCategoryMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(UnitCategoryMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<UnitCategoryMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
