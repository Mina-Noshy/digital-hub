using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IGenderMasterRepository : IBaseRepository
{
    IQueryable<GenderMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<GenderMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(GenderMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(GenderMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(GenderMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<GenderMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
