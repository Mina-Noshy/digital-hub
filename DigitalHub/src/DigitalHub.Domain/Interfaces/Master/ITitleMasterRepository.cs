using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface ITitleMasterRepository : IBaseRepository
{
    IQueryable<TitleMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<TitleMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(TitleMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(TitleMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(TitleMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<TitleMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
