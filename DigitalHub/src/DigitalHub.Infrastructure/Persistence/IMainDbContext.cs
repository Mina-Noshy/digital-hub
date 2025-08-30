using DigitalHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DigitalHub.Infrastructure.Persistence;

/// <summary>
/// Interface for database context operations
/// </summary>
public interface IMainDbContext : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Retrieves the table name associated with the specified entity type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public string? GetTableName<T>() where T : BaseEntity;

    /// <summary>
    /// Gets the database facade for the context
    /// </summary>
    DatabaseFacade Database { get; }

    /// <summary>
    /// Begins tracking the given entity
    /// </summary>
    EntityEntry Entry(object entity);

    /// <summary>
    /// Gets a DbSet for access to entities of the given type
    /// </summary>
    DbSet<T> Set<T>() where T : class;

    /// <summary>
    /// Saves all changes made in this context to the database
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new database transaction
    /// </summary>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the given transaction
    /// </summary>
    Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back the given transaction
    /// </summary>
    Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);
}