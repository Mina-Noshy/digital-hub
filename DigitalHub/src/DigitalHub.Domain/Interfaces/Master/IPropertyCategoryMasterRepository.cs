using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IPropertyCategoryMasterRepository : IBaseRepository
{
    IQueryable<PropertyCategoryMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<PropertyCategoryMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(PropertyCategoryMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(PropertyCategoryMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(PropertyCategoryMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<PropertyCategoryMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
