namespace UsersCollectionAPI.Commands;

public interface ICommandAsync<T, TResult>
{
    Task<TResult> ExecuteAsync(T model);
}

public interface ICommand<T, TResult>
{
    TResult Execute(T model);
}
