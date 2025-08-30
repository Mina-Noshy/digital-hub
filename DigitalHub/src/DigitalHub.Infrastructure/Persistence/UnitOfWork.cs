using DigitalHub.Domain.Entities;
using DigitalHub.Domain.Interfaces.Common.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace DigitalHub.Infrastructure.Persistence;


/// <summary>
/// Implementation of Unit of Work pattern for database operations
/// </summary>
public sealed class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
{
    private readonly IMainDbContext _context;
    private IDbContextTransaction? _currentTransaction;
    private readonly IRepositoryFactory _repositoryFactory;
    private bool _disposed;

    public UnitOfWork(IMainDbContext context, IRepositoryFactory repositoryFactory)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _repositoryFactory = repositoryFactory;
    }

    /// <summary>
    /// Retrieves the table name associated with the specified entity type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public string? GetTableName<T>() where T : BaseEntity
        => _context.GetTableName<T>();

    /// <summary>
    /// Gets a repository for database operations with ignore query filters option
    /// </summary>
    /// <returns>An implementation of IRepository</returns>
    public IRepository Repository(bool ignoreQueryFilters = false)
        => _repositoryFactory.Create(ignoreQueryFilters);

    /// <summary>
    /// Saves all changes made in this context to the database
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete</param>
    /// <returns>The number of state entries written to the database</returns>
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Begins a new database transaction
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress");
        }

        _currentTransaction = await _context.Database
            .BeginTransactionAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Commits the current transaction
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No active transaction to commit");
        }

        try
        {
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            await _currentTransaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
            throw;
        }
        finally
        {
            await DisposeTransactionAsync().ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Rolls back the current transaction
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            try
            {
                await _currentTransaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                await DisposeTransactionAsync().ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Executes the provided action within a transaction
    /// </summary>
    /// <param name="action">The action to execute</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        var transactionCreatedHere = _currentTransaction == null;

        try
        {
            if (transactionCreatedHere)
            {
                await BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
            }

            await action().ConfigureAwait(false);

            if (transactionCreatedHere)
            {
                await CommitTransactionAsync(cancellationToken).ConfigureAwait(false);
            }
        }
        catch
        {
            if (transactionCreatedHere)
            {
                await RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
            }
            throw;
        }
    }

    /// <summary>
    /// Executes the provided function within a transaction and returns its result
    /// </summary>
    /// <typeparam name="TResult">The type of the result</typeparam>
    /// <param name="func">The function to execute</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete</param>
    /// <returns>The result of the function</returns>
    public async Task<TResult> ExecuteInTransactionAsync<TResult>(
        Func<Task<TResult>> func,
        CancellationToken cancellationToken = default)
    {
        var transactionCreatedHere = _currentTransaction == null;

        try
        {
            if (transactionCreatedHere)
            {
                await BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
            }

            var result = await func().ConfigureAwait(false);

            if (transactionCreatedHere)
            {
                await CommitTransactionAsync(cancellationToken).ConfigureAwait(false);
            }

            return result;
        }
        catch
        {
            if (transactionCreatedHere)
            {
                await RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
            }
            throw;
        }
    }

    private async Task DisposeTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.DisposeAsync().ConfigureAwait(false);
            _currentTransaction = null;
        }
    }

    /// <summary>
    /// Disposes the current context and transaction
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Asynchronously disposes the current context and transaction
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        Dispose(false);
        GC.SuppressFinalize(this);
    }

    private async ValueTask DisposeAsyncCore()
    {
        if (!_disposed)
        {
            await DisposeTransactionAsync().ConfigureAwait(false);
            _disposed = true;
        }
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
            _disposed = true;
        }
    }

    ~UnitOfWork() => Dispose(false);
}