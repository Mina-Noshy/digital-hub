using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IMaritalStatusMasterRepository : IBaseRepository
{
    IQueryable<MaritalStatusMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<MaritalStatusMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(MaritalStatusMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(MaritalStatusMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(MaritalStatusMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<MaritalStatusMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
