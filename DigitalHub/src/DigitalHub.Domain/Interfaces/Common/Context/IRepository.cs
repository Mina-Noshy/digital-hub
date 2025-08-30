using DigitalHub.Domain.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DigitalHub.Domain.Interfaces.Common.Context;

public interface IRepository
{

    #region Light Collection With Pagination
    IQueryable<T> GetLightCollection<T>(Expression<Func<T, bool>>? filterExpression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderByExpression = null,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken cancellationToken = default) where T : BaseEntity;

    #endregion

    /****************************************************************************************************************/

    #region Entity All Collection Without Pagination
    IQueryable<T> GetAll<T>(Expression<Func<T, bool>> filterExpression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderByExpression = null,
        params string[] includeProperties) where T : BaseEntity;

    Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> filterExpression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderByExpression = null,
        CancellationToken cancellationToken = default,
        params string[] includeProperties) where T : BaseEntity;

    #endregion

    /****************************************************************************************************************/

    #region Entity Find Collection With Pagination
    IQueryable<T> Find<T>(Expression<Func<T, bool>> filterExpression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderByExpression = null,
        int? pageNumber = null,
        int? pageSize = null,
        params string[] includeProperties) where T : BaseEntity;

    Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> filterExpression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderByExpression = null,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken cancellationToken = default,
        params string[] includeProperties) where T : BaseEntity;

    #endregion

    /****************************************************************************************************************/

    #region Select Collection With Pagination

    Task<IEnumerable<TResult>> SelectAsync<T, TResult>(
        Expression<Func<T, bool>> filterExpression,
        Expression<Func<T, TResult>> selector,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderByExpression = null,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken cancellationToken = default,
        params string[] includeProperties) where T : BaseEntity;

    #endregion

    /****************************************************************************************************************/

    #region Single Entity
    Task<T?> GetByIdAsync<T>(long id,
        CancellationToken cancellationToken = default,
        params string[] includeProperties) where T : BaseEntity;

    Task<T?> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default,
        params string[] includeProperties) where T : BaseEntity;

    Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default,
        params string[] includeProperties) where T : BaseEntity;

    #endregion

    /****************************************************************************************************************/

    #region Add Entity
    void Add<T>(T entity) where T : BaseEntity;

    Task AddRangeAsync<T>(IEnumerable<T> entities,
        CancellationToken cancellationToken = default) where T : BaseEntity;

    #endregion

    /****************************************************************************************************************/

    #region Update Entity
    void Update<T>(T entity) where T : BaseEntity;

    void Update<T>(IEnumerable<T> entities) where T : BaseEntity;

    #endregion

    /****************************************************************************************************************/

    #region Delete Entity
    void SoftDelete<T>(T entity) where T : BaseEntity;
    void SoftDelete<T>(IEnumerable<T> entities) where T : BaseEntity;

    void HardDelete<T>(T entity) where T : BaseEntity;
    void HardDelete<T>(IEnumerable<T> entities) where T : BaseEntity;

    #endregion

    /****************************************************************************************************************/

    #region Count Entity
    Task<int> CountAsync<T>(Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default) where T : BaseEntity;

    #endregion

    /****************************************************************************************************************/

    #region Check Exists Entity
    Task<bool> AnyAsync<T>(Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default) where T : BaseEntity;

    #endregion

    /****************************************************************************************************************/

    #region Raw SQL
    Task<int> ExecuteAsync(string sql, DynamicParameters? parameters, CancellationToken cancellationToken = default);
    Task<T?> ExecuteScalarAsync<T>(string sql, DynamicParameters? parameters, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, DynamicParameters? parameters, CancellationToken cancellationToken = default);
    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, DynamicParameters? parameters, CancellationToken cancellationToken = default);
    Task<IEnumerable<dynamic>> QueryAsync(string sql, DynamicParameters? parameters, CancellationToken cancellationToken = default);
    Task<dynamic?> QueryFirstOrDefaultAsync(string sql, DynamicParameters? parameters, CancellationToken cancellationToken = default);

    #endregion

    /****************************************************************************************************************/

    #region Max, Min
    Task<TResult?> MaxAsync<T, TResult>(
        Expression<Func<T, bool>> filterExpression,
        Expression<Func<T, TResult>> selector,
        CancellationToken cancellationToken = default)
        where T : BaseEntity;

    Task<TResult?> MinAsync<T, TResult>(
        Expression<Func<T, bool>> filterExpression,
        Expression<Func<T, TResult>> selector,
        CancellationToken cancellationToken = default)
        where T : BaseEntity;

    #endregion

    /****************************************************************************************************************/

    #region ExecuteUpdate, ExecuteDelete
    Task<int> ExecuteUpdateAsync<T>(
        Expression<Func<T, bool>> filterExpression,
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression,
        CancellationToken cancellationToken = default)
        where T : BaseEntity;

    Task<int> ExecuteSoftDeleteAsync<T>(
        Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default) where T : BaseEntity;

    Task<int> ExecuteHardDeleteAsync<T>(
        Expression<Func<T, bool>> filterExpression,
        CancellationToken cancellationToken = default) where T : BaseEntity;

    #endregion

    /****************************************************************************************************************/

}
