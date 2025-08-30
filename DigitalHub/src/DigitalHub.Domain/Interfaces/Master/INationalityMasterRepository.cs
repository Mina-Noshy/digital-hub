using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface INationalityMasterRepository : IBaseRepository
{
    IQueryable<NationalityMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<NationalityMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(NationalityMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(NationalityMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(NationalityMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<NationalityMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
