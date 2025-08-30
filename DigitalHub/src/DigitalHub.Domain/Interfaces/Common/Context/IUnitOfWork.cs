using DigitalHub.Domain.Entities;

namespace DigitalHub.Domain.Interfaces.Common.Context;

/// <summary>
/// Interface for Unit of Work pattern implementation
/// </summary>
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Retrieves the table name associated with the specified entity type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public string? GetTableName<T>() where T : BaseEntity;

    /// <summary>
    /// Gets a repository for database operations with ignore query filters option
    /// </summary>
    /// <returns>An implementation of IRepository</returns>
    IRepository Repository(bool ignoreQueryFilters = false);

    /// <summary>
    /// Saves all changes made in this context to the database
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete</param>
    /// <returns>The number of state entries written to the database</returns>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new database transaction
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the current transaction
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back the current transaction
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the provided action within a transaction
    /// </summary>
    /// <param name="action">The action to execute</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the provided function within a transaction and returns its result
    /// </summary>
    /// <typeparam name="TResult">The type of the result</typeparam>
    /// <param name="func">The function to execute</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete</param>
    /// <returns>The result of the function</returns>
    Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken = default);
}