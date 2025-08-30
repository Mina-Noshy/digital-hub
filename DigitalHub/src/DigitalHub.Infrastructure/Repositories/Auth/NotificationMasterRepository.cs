using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;
using DigitalHub.Domain.Utilities;

namespace DigitalHub.Infrastructure.Repositories.Auth;

internal class NotificationMasterRepository(IUnitOfWork _unitOfWork) : INotificationMasterRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public async Task<NotificationMaster?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository(true).GetByIdAsync<NotificationMaster>(id, cancellationToken);
    }
    public async Task<bool> AddAsync(NotificationMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository(true).Add(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<int> UnReadCountAsync(long userId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository(true).CountAsync<NotificationMaster>(
            x => x.UserId == userId && !x.IsRead,
            cancellationToken);
    }
    public async Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository(true).CountAsync<NotificationMaster>(
            x => x.UserId == queryParams.ParentId && (string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Title.Contains(queryParams.SearchTerm)),
            cancellationToken);
    }
    public IQueryable<NotificationMaster> GetPaged(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository(true).Find<NotificationMaster>(
            x => x.UserId == queryParams.ParentId && (string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Title.Contains(queryParams.SearchTerm)),
            o => o.OrderByDescending(x => x.Id),
            queryParams.PageNumber,
            queryParams.PageSize);
    }

    public async Task<bool> AddRangeAsync(IEnumerable<NotificationMaster> entities, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository(true).AddRangeAsync(entities);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }

    public async Task<bool> DeleteRangeAsync(long userId, long[] ids, CancellationToken cancellationToken)
    {
        var effectedRows =
            await _unitOfWork.Repository(true).ExecuteHardDeleteAsync<NotificationMaster>(x => ids.Contains(x.Id) && x.UserId == userId, cancellationToken);

        return effectedRows > 0;
    }

    public async Task<bool> MarkRangeAsReadAsync(long userId, long[] ids, CancellationToken cancellationToken)
    {
        var effectedRows =
            await _unitOfWork.Repository(true).ExecuteUpdateAsync<NotificationMaster>(
                x => ids.Contains(x.Id) && x.UserId == userId,
                x => x.SetProperty(p => p.IsRead, true).SetProperty(p => p.ReadAt, DateTimeProvider.UtcNow),
                cancellationToken);

        return effectedRows > 0;
    }
}
