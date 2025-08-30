using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Infrastructure.Repositories;

namespace DigitalHub.Infrastructure.Persistence;


internal class RepositoryFactory(IMainDbContext context) : IRepositoryFactory
{
    public IRepository Create(bool ignoreQueryFilters = false)
        => new Repository(context, ignoreQueryFilters);
}