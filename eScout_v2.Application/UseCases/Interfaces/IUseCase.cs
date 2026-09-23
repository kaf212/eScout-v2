namespace eScout_v2.Application.UseCases.Interfaces;

public interface IUseCase<in TInput, TOutput>
{
    public ValueTask<TOutput> ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}

public interface IUseCase<TOutput>
{
    public ValueTask<TOutput> ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IUseCaseVoid<in TInput> where TInput : class
{
    public Task ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}

public interface IUseCaseVoid
{
    public Task ExecuteAsync(CancellationToken cancellationToken = default);
}