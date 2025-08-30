using DigitalHub.Domain.Entities;

namespace DigitalHub.Domain.Exceptions;

public sealed class EntityNotFoundException<T> : NotFoundException where T : BaseEntity
{
    public EntityNotFoundException(long id)
        : base($"The entity '{typeof(T).Name}' with the identifier '{id}' was not found.")
    {
    }
}
