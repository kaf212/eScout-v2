namespace eScout_v2.Application.UseCases.Interfaces;

public interface IUseCase<in TInput, TOutput> where TInput : class
{
    public ValueTask<TOutput> ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}

public interface IUseCase<TOutput>
{
    public ValueTask<TOutput> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IUseCaseVoid<in TInput> where TInput : class
{
    public ValueTask ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}

public interface IUseCaseVoid
{
    public ValueTask ExecuteAsync(CancellationToken cancellationToken = default);
}