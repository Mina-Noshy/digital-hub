using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;

namespace DigitalHub.Infrastructure.Repositories.Auth;

internal class UserLoginHistoryMasterRepository(IUnitOfWork _unitOfWork) : IUserLoginHistoryMasterRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public async Task<IEnumerable<UserLoginHistoryMaster>> GetByUserIdAsync(long userId, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().FindAsync<UserLoginHistoryMaster>(
                x => x.UserId == userId,
                o => o.OrderByDescending(x => x.Id),
                1,
                100,
                cancellationToken);
    }
    public async Task<bool> AddAsync(UserLoginHistoryMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Add(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
}
