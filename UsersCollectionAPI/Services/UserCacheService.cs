using System.Collections.Concurrent;
using System.Timers;
using UsersCollectionAPI.Model.Entities;
using UsersCollectionAPI.Model.Infrastructure.Interfaces;
using Timer = System.Timers.Timer;

namespace UsersCollectionAPI.Services;

public class UserCacheService
{
    private readonly IDictionary<int, User> _userCache = new ConcurrentDictionary<int, User>();
    
    private readonly Timer _timer;

    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    private const int CacheUpdateInterval = 10 * 60 * 1000;
    
    public UserCacheService(IServiceScopeFactory serviceScopeFactory)
    {
        _timer = new Timer(CacheUpdateInterval);
        _timer.Elapsed += TimerElapsed;
        _timer.AutoReset = true;
        _timer.Start();
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void Init()
    {
        ReloadCache();
    }

    public User? GetItem(int id)
    {
        if (_userCache.TryGetValue(id, out User? user))
            return user;

        return null;
    }

    public void UpdateCache(User user)
    {
        _userCache[user.Id] = user;
    }
    
    public void RemoveFromCache(int id)
    {
        _userCache.Remove(id);
    }

    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        ReloadCache();
    }

    private void ReloadCache()
    {
        _userCache.Clear();
        
        using var scope = _serviceScopeFactory.CreateScope();
        
        IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        IEnumerable<User> users = unitOfWork.Users.GetAll();
        foreach (User user in users)
            _userCache[user.Id] = user;
    }
}