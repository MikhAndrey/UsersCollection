using UsersCollectionAPI.Model.Exceptions;

namespace UsersCollectionAPI.Utils;

public static class Constants
{
    public static readonly Dictionary<Type, int> ExceptionCodes = new()
    {
        { typeof(UserDuplicateException), 1 },
        { typeof(UserNotFoundException), 1 }
    };

    public const int CacheUpdateInterval = 10 * 60 * 1000;

    public static readonly Func<int, string> UserDuplicateDefaultMessage = id => $"User with id {id} already exists";
    public static readonly Func<int, string> UserNotFoundDefaultMessage = id => $"User with id {id} not found";
}
