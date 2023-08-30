using UsersCollectionAPI.Utils;

namespace UsersCollectionAPI.Model.Exceptions;

public class UserDuplicateException : ApplicationException
{
    public UserDuplicateException(string message) : base(message, Constants.ExceptionCodes[typeof(UserDuplicateException)])
    {
    }
}
