namespace DigitalHub.Domain.Interfaces.Common.Context;

public interface IBaseRepository
{
    IUnitOfWork UnitOfWork { get; }
}
