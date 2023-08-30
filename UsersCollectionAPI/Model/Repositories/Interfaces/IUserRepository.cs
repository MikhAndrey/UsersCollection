using UsersCollectionAPI.Model.Entities;

namespace UsersCollectionAPI.Model.Repositories.Interfaces;

public interface IUserRepository
{
    IQueryable<User> GetAll();
    Task<User?> GetByIdAsync(int id);
    bool Exists(int id);
    Task AddAsync(User user);
    void Remove(User user);
}
