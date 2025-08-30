namespace DigitalHub.Domain.Interfaces.Common.Context;

public interface IRepositoryFactory
{
    IRepository Create(bool ignoreQueryFilters = false);
}
