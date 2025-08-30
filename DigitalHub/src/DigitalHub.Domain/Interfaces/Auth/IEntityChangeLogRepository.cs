using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Auth;

public interface IEntityChangeLogRepository : IBaseRepository
{
    Task<IEnumerable<EntityChangeLog>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
