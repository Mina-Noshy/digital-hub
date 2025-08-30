using DigitalHub.Domain.Entities;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Infrastructure.Persistence;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace DigitalHub.Infrastructure.Repositories;

internal class Repository(IMainDbContext _context, bool _ignoreQueryFilters) : IRepository
{

    #region Light Collection With Pagination
    public IQueryable<T> GetLightCollection<T>(Expression<Func<T, bool>>? filterExpression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderByExpression = null,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken cancellationToken = default) where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>();

        // Apply filterExpression if provided
        if (filterExpression != null)
            query = query.Where(filterExpression);

        // Apply additional conditions for IsDeleted
        // query = query.Where(x => !x.IsDeleted);

        // Apply orderByExpression if provided
        if (orderByExpression == null)
        {
            orderByExpression = x => x.OrderByDescending(s => s.Id);
        }
        query = orderByExpression(query);

        // Apply pagination
        query = ApplyPagination(query, pageNumber, pageSize);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return query;
    }

    #endregion

    /****************************************************************************************************************/

    #region Entity All Collection Without Pagination
    public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> filterExpression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderByExpression = null,
        params string[] includeProperties) where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Apply additional conditions for IsDeleted
        // query = query.Where(x => !x.IsDeleted);

        // Enable query splitting for multiple includes
        if (includeProperties.Length > 1)
        {
            query = query.AsSplitQuery();
        }

        // Include properties
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        // Apply orderByExpression if provided
        if (orderByExpression == null)
        {
            orderByExpression = x => x.OrderByDescending(s => s.Id);
        }

        query = orderByExpression(query);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return query;
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> filterExpression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderByExpression = null,
        CancellationToken cancellationToken = default,
        params string[] includeProperties) where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Apply additional conditions for IsDeleted
        // query = query.Where(x => !x.IsDeleted);

        // Enable query splitting for multiple includes
        if (includeProperties.Length > 1)
        {
            query = query.AsSplitQuery();
        }

        // Include properties
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        // Apply orderByExpression if provided
        if (orderByExpression == null)
        {
            orderByExpression = x => x.OrderByDescending(s => s.Id);
        }

        query = orderByExpression(query);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.ToListAsync(cancellationToken);
    }

    #endregion

    /****************************************************************************************************************/

    #region Entity Find Collection With Pagination
    public IQueryable<T> Find<T>(Expression<Func<T, bool>> filterExpression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderByExpression = null,
        int? pageNumber = null,
        int? pageSize = null,
        params string[] includeProperties) where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Apply additional conditions for IsDeleted
        // query = query.Where(x => !x.IsDeleted);

        // Enable query splitting for multiple includes
        if (includeProperties.Length > 1)
        {
            query = query.AsSplitQuery();
        }

        // Include properties
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        // Apply orderByExpression if provided
        if (orderByExpression == null)
        {
            orderByExpression = x => x.OrderByDescending(s => s.Id);
        }

        query = orderByExpression(query);

        // Apply pagination
        query = ApplyPagination(query, pageNumber, pageSize);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return query;
    }

    public async Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> filterExpression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderByExpression = null,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken cancellationToken = default,
        params string[] includeProperties) where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Apply additional conditions for IsDeleted
        // query = query.Where(x => !x.IsDeleted);

        // Enable query splitting for multiple includes
        if (includeProperties.Length > 1)
        {
            query = query.AsSplitQuery();
        }

        // Include properties
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        // Apply orderByExpression if provided
        if (orderByExpression == null)
        {
            orderByExpression = x => x.OrderByDescending(s => s.Id);
        }

        query = orderByExpression(query);

        // Apply pagination
        query = ApplyPagination(query, pageNumber, pageSize);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.ToListAsync(cancellationToken);
    }

    #endregion

    /****************************************************************************************************************/

    #region Select Collection With Pagination

    public async Task<IEnumerable<TResult>> SelectAsync<T, TResult>(
    Expression<Func<T, bool>> filterExpression,
    Expression<Func<T, TResult>> selector,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderByExpression = null,
    int? pageNumber = null,
    int? pageSize = null,
    CancellationToken cancellationToken = default,
    params string[] includeProperties) where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Apply additional conditions for IsDeleted
        // query = query.Where(x => !x.IsDeleted);

        // Enable query splitting for multiple includes
        if (includeProperties.Length > 1)
        {
            query = query.AsSplitQuery();
        }

        // Include properties
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        // Apply orderByExpression if provided
        if (orderByExpression == null)
        {
            orderByExpression = x => x.OrderByDescending(s => s.Id);
        }

        query = orderByExpression(query);

        query = ApplyPagination(query, pageNumber, pageSize);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.Select(selector).ToListAsync(cancellationToken);
    }

    #endregion

    /****************************************************************************************************************/

    #region Single Entity
    public async Task<T?> GetByIdAsync<T>(long id,
        CancellationToken cancellationToken = default,
        params string[] includeProperties) where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>();

        // Enable query splitting for multiple includes
        if (includeProperties.Length > 1)
        {
            query = query.AsSplitQuery();
        }

        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        // Apply additional conditions for IsDeleted
        // query = query.Where(x => !x.IsDeleted);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<T?> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default,
        params string[] includeProperties) where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Apply additional conditions for IsDeleted
        // query = query.Where(x => !x.IsDeleted);

        // Enable query splitting for multiple includes
        if (includeProperties.Length > 1)
        {
            query = query.AsSplitQuery();
        }

        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default,
        params string[] includeProperties) where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Apply additional conditions for IsDeleted
        // query = query.Where(x => !x.IsDeleted);

        // Enable query splitting for multiple includes
        if (includeProperties.Length > 1)
        {
            query = query.AsSplitQuery();
        }

        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    #endregion

    /****************************************************************************************************************/

    #region Add Entity
    public void Add<T>(T entity) where T : BaseEntity
    {
        _context.Set<T>().Add(entity);
    }

    public async Task AddRangeAsync<T>(IEnumerable<T> entities,
        CancellationToken cancellationToken = default) where T : BaseEntity
    {
        await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
    }

    #endregion

    /****************************************************************************************************************/

    #region Update Entity
    public void Update<T>(T entity) where T : BaseEntity
    {
        if (entity.Id < 1)
            return;

        _context.Set<T>().Update(entity);
    }

    public void Update<T>(IEnumerable<T> entities) where T : BaseEntity
    {
        _context.Set<T>().UpdateRange(entities.Where(x => x.Id > 0));
    }

    #endregion

    /****************************************************************************************************************/

    #region Delete Entity
    public void SoftDelete<T>(T entity) where T : BaseEntity
    {
        entity.IsDeleted = true;
        Update<T>(entity);

        //_context.Set<T>().Remove(entity);
    }

    public void SoftDelete<T>(IEnumerable<T> entities) where T : BaseEntity
    {
        foreach (var item in entities)
        {
            item.IsDeleted = true;
        }
        Update<T>(entities);

        //_context.Set<T>().RemoveRange(entities);
    }


    public void HardDelete<T>(T entity) where T : BaseEntity
    {
        _context.Set<T>().Remove(entity);
    }

    public void HardDelete<T>(IEnumerable<T> entities) where T : BaseEntity
    {
        _context.Set<T>().RemoveRange(entities);
    }

    #endregion

    /****************************************************************************************************************/

    #region Count Entity
    public async Task<int> CountAsync<T>(Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default) where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Apply additional conditions for IsDeleted
        // query = query.Where(x => !x.IsDeleted);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.CountAsync(cancellationToken);
    }

    #endregion

    /****************************************************************************************************************/

    #region Check Exists Entity
    public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default) where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Apply additional conditions for IsDeleted
        // query = query.Where(x => !x.IsDeleted);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.AnyAsync(filterExpression, cancellationToken);
    }

    #endregion

    /****************************************************************************************************************/

    #region Private Apply Pagination
    private IQueryable<T> ApplyPagination<T>(IQueryable<T> query, int? pageNumber, int? pageSize) where T : BaseEntity
    {
        if (!pageNumber.HasValue)
        {
            pageNumber = 1;
        }
        if (!pageSize.HasValue)
        {
            pageSize = 10;
        }

        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        pageSize = pageSize > 10000 ? 10000 : pageSize;

        int skip = (pageNumber.Value - 1) * pageSize.Value;
        return query.Skip(skip).Take(pageSize.Value);
    }

    #endregion

    /****************************************************************************************************************/

    #region Raw SQL

    public async Task<int> ExecuteAsync(string sql, DynamicParameters? parameters, CancellationToken cancellationToken = default)
    {
        var connection = _context.Database.GetDbConnection();
        var transaction = _context.Database.CurrentTransaction?.GetDbTransaction();

        return await connection.ExecuteAsync(sql, param: parameters, transaction: transaction);
    }


    public async Task<T?> ExecuteScalarAsync<T>(string sql, DynamicParameters? parameters, CancellationToken cancellationToken = default)
    {
        var connection = _context.Database.GetDbConnection();
        var transaction = _context.Database.CurrentTransaction?.GetDbTransaction();

        return await connection.ExecuteScalarAsync<T>(sql, param: parameters, transaction: transaction);
    }


    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, DynamicParameters? parameters, CancellationToken cancellationToken = default)
    {
        var connection = _context.Database.GetDbConnection();
        var transaction = _context.Database.CurrentTransaction?.GetDbTransaction();

        return await connection.QueryAsync<T>(sql, param: parameters, transaction: transaction);
    }


    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, DynamicParameters? parameters, CancellationToken cancellationToken = default)
    {
        var connection = _context.Database.GetDbConnection();
        var transaction = _context.Database.CurrentTransaction?.GetDbTransaction();

        return await connection.QueryFirstOrDefaultAsync<T>(sql, param: parameters, transaction: transaction);
    }


    public async Task<IEnumerable<dynamic>> QueryAsync(string sql, DynamicParameters? parameters, CancellationToken cancellationToken = default)
    {
        var connection = _context.Database.GetDbConnection();
        var transaction = _context.Database.CurrentTransaction?.GetDbTransaction();

        return await connection.QueryAsync<dynamic>(sql, param: parameters, transaction: transaction);
    }


    public async Task<dynamic?> QueryFirstOrDefaultAsync(string sql, DynamicParameters? parameters, CancellationToken cancellationToken = default)
    {
        var connection = _context.Database.GetDbConnection();
        var transaction = _context.Database.CurrentTransaction?.GetDbTransaction();

        return await connection.QueryFirstOrDefaultAsync<dynamic>(sql, param: parameters, transaction: transaction);
    }


    #endregion

    /****************************************************************************************************************/

    #region Max, Min
    public async Task<TResult?> MaxAsync<T, TResult>(
        Expression<Func<T, bool>> filterExpression,
        Expression<Func<T, TResult>> selector,
        CancellationToken cancellationToken = default)
        where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Optionally filter deleted records
        // query = query.Where(x => !x.IsDeleted);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.MaxAsync(selector, cancellationToken);
    }

    public async Task<TResult?> MinAsync<T, TResult>(
        Expression<Func<T, bool>> filterExpression,
        Expression<Func<T, TResult>> selector,
        CancellationToken cancellationToken = default)
        where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Optionally filter deleted records
        // query = query.Where(x => !x.IsDeleted);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.MinAsync(selector, cancellationToken);
    }

    #endregion

    /****************************************************************************************************************/

    #region ExecuteUpdate, ExecuteDelete
    public async Task<int> ExecuteUpdateAsync<T>(
        Expression<Func<T, bool>> filterExpression,
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression,
        CancellationToken cancellationToken = default)
        where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Optionally filter deleted records
        // query = query.Where(x => !x.IsDeleted);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.ExecuteUpdateAsync(updateExpression, cancellationToken);
    }

    public async Task<int> ExecuteSoftDeleteAsync<T>(
        Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default)
        where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Optionally filter deleted records
        // query = query.Where(x => !x.IsDeleted);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDeleted, true), cancellationToken);
    }

    public async Task<int> ExecuteHardDeleteAsync<T>(
        Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default)
        where T : BaseEntity
    {
        IQueryable<T> query = _context.Set<T>().Where(filterExpression);

        // Optionally filter deleted records
        // query = query.Where(x => !x.IsDeleted);

        // Ignore query filters
        if (_ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.ExecuteDeleteAsync(cancellationToken);
    }
    #endregion

    /****************************************************************************************************************/


}
