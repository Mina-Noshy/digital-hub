using System.Data.Common;

namespace DigitalHub.Domain.Interfaces.Common.Context;

public interface IDatabaseRouter
{
    public DbConnection GetRoutedConnection();
    public DbConnection GetBaseConnection();
}
