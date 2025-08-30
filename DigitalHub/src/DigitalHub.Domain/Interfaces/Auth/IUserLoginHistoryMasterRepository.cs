using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;

namespace DigitalHub.Domain.Interfaces.Auth;

public interface IUserLoginHistoryMasterRepository : IBaseRepository
{
    Task<IEnumerable<UserLoginHistoryMaster>> GetByUserIdAsync(long userId, CancellationToken cancellationToken);
    Task<bool> AddAsync(UserLoginHistoryMaster entity, CancellationToken cancellationToken);
}
