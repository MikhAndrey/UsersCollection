using System.Collections.Concurrent;
using System.Timers;
using UsersCollectionAPI.Model.Entities;
using UsersCollectionAPI.Model.Infrastructure.Interfaces;
using UsersCollectionAPI.Utils;
using Timer = System.Timers.Timer;

namespace UsersCollectionAPI.Services;

public class UserCacheService
{
    private readonly IDictionary<int, User> _userCache = new ConcurrentDictionary<int, User>();
    
    private readonly Timer _timer;

    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public UserCacheService(IServiceScopeFactory serviceScopeFactory)
    {
        _timer = new Timer(Constants.CacheUpdateInterval);
        _timer.Elapsed += TimerElapsed;
        _timer.AutoReset = true;
        _timer.Start();
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void Init()
    {
        Update();
    }

    public User? Get(int id)
    {
        if (_userCache.TryGetValue(id, out User? user))
        {
            return user;
        }
        
        return null;
    }

    public void Update(User user)
    {
        _userCache[user.Id] = user;
    }
    
    public void Remove(int id)
    {
        _userCache.Remove(id);
    }

    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        Update();
    }

    private void Update()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        
        IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        IEnumerable<User> users = unitOfWork.Users.GetAll();
        foreach (User user in users)
        {
            _userCache[user.Id] = user;
        }
    }
}