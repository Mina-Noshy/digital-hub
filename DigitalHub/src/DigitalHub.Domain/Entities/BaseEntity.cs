using DigitalHub.Domain.Utilities;
using System.ComponentModel.DataAnnotations;

namespace DigitalHub.Domain.Entities;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }

    public string? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    public string? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Gets a human-readable string indicating how long ago this entity was created.
    /// Returns values like "5 seconds ago", "3 hours ago", "2 months ago", etc.
    /// If <see cref="CreatedAt"/> is null, defaults to "0 seconds ago".
    /// </summary>
    public string Since => DateTimeHelper.FormatTimeAgo(CreatedAt ?? DateTimeProvider.UtcNow);

    /// <summary>
    /// Updates an entity's properties with values from a DTO that have matching property names and types.
    /// </summary>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="dto">The data transfer object containing updated values.</param>
    /// <param name="entity">The entity to be updated.</param>
    /// <exception cref="ArgumentNullException">Thrown when either the DTO or entity is null.</exception>
    public void UpdateFromDto<TDto>(TDto dto) where TDto : BaseDto
    {
        if (dto is null)
        {
            throw new ArgumentNullException(nameof(dto), "DTO cannot be null.");
        }

        var dtoProperties = typeof(TDto).GetProperties();
        var entityProperties = GetType().GetProperties();

        foreach (var dtoProperty in dtoProperties)
        {
            // Find the corresponding property in the entity
            var entityProperty = entityProperties.FirstOrDefault(p => p.Name == dtoProperty.Name && p.PropertyType == dtoProperty.PropertyType);

            // Update the entity property if it exists and is writable
            if (entityProperty != null && entityProperty.CanWrite)
            {
                var value = dtoProperty.GetValue(dto);
                entityProperty.SetValue(this, value);
            }
        }
    }

}
