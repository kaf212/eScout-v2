namespace eScout_v2.Application.Exceptions;

public class EntityNotFoundException<TEntity> : Exception
{
    public EntityNotFoundException() : base($"Could not find entity of type \"{nameof(TEntity)}\".") {}

    public EntityNotFoundException(Guid id) :
        base($"Could not find entity of type \"{nameof(TEntity)}\" with ID {id}.") {}
    
    public EntityNotFoundException(string message) : base(message) {}

}