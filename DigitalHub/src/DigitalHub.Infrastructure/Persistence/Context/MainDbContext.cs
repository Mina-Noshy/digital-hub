using DigitalHub.Domain.Entities;
using DigitalHub.Domain.Entities.Auth.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace DigitalHub.Infrastructure.Persistence.Context;

public partial class MainDbContext :
    IdentityDbContext
        <
        UserMaster,
        RoleMaster,
        long,
        UserClaimsMaster,
        UserRoleMaster,
        UserLoginsMaster,
        RoleClaimsMaster,
        UserTokensMaster
        >,
    IMainDbContext
{
    private IDbContextTransaction? _currentTransaction;
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }



    public string? GetTableName<T>() where T : BaseEntity
        => Model.FindEntityType(typeof(T))?.GetTableName();



    // Override ***************************************************************************
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure customizations for Identity tables
        modelBuilder.Entity<UserMaster>().ToTable("UserMaster");
        modelBuilder.Entity<RoleMaster>().ToTable("RoleMaster");
        modelBuilder.Entity<UserRoleMaster>().ToTable("UserRoleMaster");
        modelBuilder.Entity<UserClaimsMaster>().ToTable("UserClaimsMaster");
        modelBuilder.Entity<UserLoginsMaster>().ToTable("UserLoginsMaster");
        modelBuilder.Entity<RoleClaimsMaster>().ToTable("RoleClaimsMaster");
        modelBuilder.Entity<UserTokensMaster>().ToTable("UserTokensMaster");

        // Apply all entities configuration
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);



        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // Check if the entity inherits from TEntity
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                // Add global filter [IsDeleted = False] to all entities
                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(CreateIsDeletedFilter(entityType.ClrType));

                // Add IsDeleted index to all entities for better performance
                modelBuilder.Entity(entityType.ClrType)
                    .HasIndex(nameof(BaseEntity.IsDeleted))
                    .HasDatabaseName($"IX_{entityType.GetTableName()}_IsDeleted");
            }
        }

    }


    // Database transaction ***************************************************************************
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            return _currentTransaction;
        }

        _currentTransaction = await Database
            .BeginTransactionAsync(cancellationToken)
            .ConfigureAwait(false);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        if (transaction != _currentTransaction)
        {
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
        }

        try
        {
            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            await DisposeTransactionAsync().ConfigureAwait(false);
        }
    }

    public async Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        if (transaction != _currentTransaction)
        {
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
        }

        try
        {
            await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            await DisposeTransactionAsync().ConfigureAwait(false);
        }
    }






    // Memory management ***************************************************************************

    private async Task DisposeTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.DisposeAsync().ConfigureAwait(false);
            _currentTransaction = null;
        }
    }

    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public override async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        Dispose(false);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (disposing)
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
        base.Dispose();
    }

    private async ValueTask DisposeAsyncCore()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.DisposeAsync().ConfigureAwait(false);
            _currentTransaction = null;
        }
    }




    // Others ***************************************************************************
    private static LambdaExpression CreateIsDeletedFilter(Type entityType)
    {
        // Create the filter: entity => !entity.IsDeleted
        var parameter = Expression.Parameter(entityType, "entity");
        var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
        var body = Expression.Equal(property, Expression.Constant(false));
        return Expression.Lambda(body, parameter);
    }


}
