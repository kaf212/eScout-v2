namespace eScout_v2.Application.Exceptions;

public class EntityNotFoundException(string message) : Exception(message);

public class EntityNotFoundException<TEntity> : EntityNotFoundException
{
    public EntityNotFoundException() : base($"Could not find entity of type \"{typeof(TEntity).Name}\".") {}

    public EntityNotFoundException(Guid id) :
        base($"Could not find entity of type \"{typeof(TEntity).Name}\" with ID {id}.") {}
}