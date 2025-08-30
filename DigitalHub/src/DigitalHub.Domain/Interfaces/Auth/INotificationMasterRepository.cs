using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Auth;

public interface INotificationMasterRepository : IBaseRepository
{
    IQueryable<NotificationMaster> GetPaged(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<NotificationMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(NotificationMaster entity, CancellationToken cancellationToken);
    Task<bool> AddRangeAsync(IEnumerable<NotificationMaster> entities, CancellationToken cancellationToken);
    Task<bool> DeleteRangeAsync(long userId, long[] ids, CancellationToken cancellationToken);
    Task<bool> MarkRangeAsReadAsync(long userId, long[] ids, CancellationToken cancellationToken);
    Task<int> UnReadCountAsync(long userId, CancellationToken cancellationToken);
}
