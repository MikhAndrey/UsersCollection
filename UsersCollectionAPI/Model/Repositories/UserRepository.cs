using Microsoft.EntityFrameworkCore;
using UsersCollectionAPI.Model.Entities;
using UsersCollectionAPI.Model.Infrastructure;
using UsersCollectionAPI.Model.Repositories.Interfaces;

namespace UsersCollectionAPI.Model.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(ApplicationDbContext context)
    {
        _users = context.Set<User>();
    }

    public IQueryable<User> GetAll()
    {
        return _users;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        User? user = await _users.FindAsync(id);
        return user;
    }
    
    public bool ExistsOrDeleted(int id)
    {
        return _users.IgnoreQueryFilters().Any(u => u.Id == id);
    }

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
    }

    public void Remove(User user)
    {
        user.Status = Status.Deleted;
    }
}
