namespace eScout_v2.Application.Exceptions;

public class IdNullException(string message) : Exception(message);

public class IdNullException<TEntity> : IdNullException 
{
    public IdNullException() : base($"Provided ID for entity of type \"{typeof(TEntity).Name}\" was null.") {}
}