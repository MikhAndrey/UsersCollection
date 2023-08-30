using UsersCollectionAPI.Model.Repositories.Interfaces;

namespace UsersCollectionAPI.Model.Infrastructure.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    Task SaveAsync();
}

