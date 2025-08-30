using DigitalHub.Domain.Interfaces.Common;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.Utilities;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace DigitalHub.Infrastructure.Persistence;

internal class DatabaseRouter(ICurrentUser _currentUser) : IDatabaseRouter
{
    private readonly object _lock = new();

    public DbConnection GetBaseConnection()
    {
        lock (_lock)
        {
            return GetSqlConnection(string.Empty);
        }
    }

    public DbConnection GetRoutedConnection()
    {
        lock (_lock)
        {
            string companyId = _currentUser.DatabaseNo;
            return GetSqlConnection(companyId);
        }
    }

    private DbConnection GetSqlConnection(string companyId)
    {
        string connectionString = ConfigurationHelper.GetConnectionString();

        if (string.IsNullOrWhiteSpace(companyId))
        {
            return new SqlConnection(connectionString);
        }

        var conBuilder = new SqlConnectionStringBuilder(connectionString);
        conBuilder.InitialCatalog = "DigitalHub" + companyId;

        return new SqlConnection(conBuilder.ToString());
    }
}