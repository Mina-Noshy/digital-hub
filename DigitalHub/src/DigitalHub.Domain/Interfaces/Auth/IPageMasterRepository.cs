using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Auth;

public interface IPageMasterRepository : IBaseRepository
{
    IQueryable<PageMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<PageMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(PageMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(PageMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(PageMaster entity, CancellationToken cancellationToken);
    IQueryable<PageMaster> GetPaged(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
