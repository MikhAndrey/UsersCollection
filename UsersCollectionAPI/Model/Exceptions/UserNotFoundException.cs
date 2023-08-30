using UsersCollectionAPI.Utils;

namespace UsersCollectionAPI.Model.Exceptions;

public class UserNotFoundException : ApplicationException
{
    public UserNotFoundException(string message) : base(message, Constants.ExceptionCodes[typeof(UserNotFoundException)])
    {
    }
}
