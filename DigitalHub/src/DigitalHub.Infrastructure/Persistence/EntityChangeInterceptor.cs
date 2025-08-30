using DigitalHub.Domain.Entities;
using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Common;
using DigitalHub.Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;

namespace DigitalHub.Infrastructure.Persistence;

public class EntityChangeInterceptor(ICurrentUser _currentUser) : SaveChangesInterceptor
{
    private List<EntityChangeLog> _entityChangeLog = new();

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if (context is null)
        {
            return base.SavingChanges(eventData, result);
        }

        LogEntityChanges(context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context is null)
        {
            return base.SavingChangesAsync(eventData, result);
        }

        LogEntityChanges(context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void LogEntityChanges(DbContext context)
    {
        var changes = context.ChangeTracker.Entries<BaseEntity>()
           .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);

        foreach (var entry in changes)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    HandleEntityAdded(context, entry);
                    //PreventNavigationPropertiesUpdate(entry);
                    break;
                case EntityState.Modified:
                    HandleEntityModified(context, entry);
                    //PreventNavigationPropertiesUpdate(entry);
                    break;
                case EntityState.Deleted:
                    HandleEntityDeleted(context, entry);
                    //PreventNavigationPropertiesUpdate(entry);
                    break;
            }
        }

        if (this._entityChangeLog.Any())
        {
            context.Set<EntityChangeLog>().AddRange(this._entityChangeLog);
            this._entityChangeLog.Clear();
        }
    }


    private void HandleEntityAdded<T>(DbContext context, EntityEntry<T> entry) where T : BaseEntity
    {
        var timestamp = DateTimeProvider.UtcNow;
        var username = _currentUser.Username;

        entry.Entity.CreatedBy = username;
        entry.Entity.CreatedAt = timestamp;

        LogDatabaseAction(context, entry, "insert");
    }

    private void HandleEntityModified<T>(DbContext context, EntityEntry<T> entry) where T : BaseEntity
    {
        // We don't delete any record from db
        // just toggle is deleted to true.

        var timestamp = DateTimeProvider.UtcNow;
        var username = _currentUser.Username;

        if (entry.Entity.IsDeleted)
        {
            HandleEntityDeleted(context, entry);
        }
        else
        {
            entry.Entity.ModifiedBy = username;
            entry.Entity.ModifiedAt = timestamp;
            LogDatabaseAction(context, entry, "update");
        }
    }

    private void HandleEntityDeleted<T>(DbContext context, EntityEntry<T> entry) where T : BaseEntity
    {
        var timestamp = DateTimeProvider.UtcNow;
        var username = _currentUser.Username;

        entry.Entity.DeletedBy = username;
        entry.Entity.DeletedAt = timestamp;
        LogDatabaseAction(context, entry, "delete");
    }

    private void LogDatabaseAction<T>(DbContext context, EntityEntry<T> entry, string action) where T : BaseEntity
    {
        var timestamp = DateTimeProvider.UtcNow;
        var userId = _currentUser.UserId;
        var username = _currentUser.Username;
        var ipAddress = _currentUser.IpAddress;
        var userAgent = _currentUser.UserAgent;
        var entityName = entry.Entity.GetType().Name;

        var oldValues = entry.State is EntityState.Modified or EntityState.Deleted
            ? SerializeDictionary(GetEntityDictionary(entry, currentValues: false))
            : string.Empty;

        var newValues = entry.State is EntityState.Added or EntityState.Modified
            ? SerializeDictionary(GetEntityDictionary(entry, currentValues: true))
            : string.Empty;

        var log = new EntityChangeLog()
        {
            UserId = userId,
            EntityId = entry.Entity.Id,
            EntityName = entityName,
            Operation = action,
            OldValues = oldValues,
            NewValues = newValues,
            IPAddress = ipAddress,
            UserAgent = userAgent,
            Timestamp = timestamp,
            ChangedBy = username,
        };

        this._entityChangeLog.Add(log);
    }
    private Dictionary<string, object?> GetEntityDictionary(EntityEntry entry, bool currentValues = true)
    {
        var propertyValues = currentValues ? entry.CurrentValues : entry.OriginalValues;

        return propertyValues.Properties
            .ToDictionary(
                p => p.Name,
                p => propertyValues[p]
            );
    }
    private string SerializeDictionary(Dictionary<string, object?> dict)
    {
        return JsonSerializer.Serialize(dict);
    }


    //private void PreventNavigationPropertiesUpdate(DbContext context, EntityEntry entry)
    //{
    //    // Ignores changes to navigation properties, ensuring only the main entity is updated.

    //    foreach (var navigation in entry.Navigations)
    //    {
    //        if (navigation.CurrentValue is IEnumerable<object> collection)
    //        {
    //            foreach (var child in collection.OfType<object>().ToList())
    //            {
    //                var childEntry = context.Entry(child);
    //                if (childEntry.State == EntityState.Modified)
    //                {
    //                    childEntry.State = EntityState.Unchanged;
    //                }
    //            }
    //        }
    //        else if (navigation.CurrentValue != null)
    //        {
    //            var childEntry = context.Entry(navigation.CurrentValue);
    //            if (childEntry.State == EntityState.Modified)
    //            {
    //                childEntry.State = EntityState.Unchanged;
    //            }
    //        }
    //    }
    //}

}
