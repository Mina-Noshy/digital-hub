using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IPropertyTypeMasterRepository : IBaseRepository
{
    IQueryable<PropertyTypeMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<PropertyTypeMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(PropertyTypeMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(PropertyTypeMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(PropertyTypeMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<PropertyTypeMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
