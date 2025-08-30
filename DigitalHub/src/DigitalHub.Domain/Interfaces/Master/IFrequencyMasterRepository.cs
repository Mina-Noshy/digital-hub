using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IFrequencyMasterRepository : IBaseRepository
{
    IQueryable<FrequencyMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<FrequencyMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(FrequencyMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(FrequencyMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(FrequencyMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<FrequencyMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
