namespace eScout_v2.Application.Exceptions;

public class IdNullException<TEntity> : Exception 
{
    public IdNullException() : base($"Provided ID for entity of type \"{nameof(TEntity)}\" was null.") {}
}