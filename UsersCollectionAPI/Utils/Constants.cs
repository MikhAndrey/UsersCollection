using UsersCollectionAPI.Model.Entities;
using UsersCollectionAPI.Model.Exceptions;
using ApplicationException = UsersCollectionAPI.Model.Exceptions.ApplicationException;

namespace UsersCollectionAPI.Utils;

public static class Constants
{
    public static readonly Dictionary<Type, int> ExceptionCodes = new()
    {
        { typeof(UserDuplicateException), 1 },
        { typeof(UserNotFoundException), 2 }
    };

    public static readonly Func<int, string> UserDuplicateDefaultMessage = id => $"User with id {id} already exists";
    public static readonly Func<int, string> UserNotFoundDefaultMessage = id => $"User with id {id} not found";
    
    public static readonly Func<User, string> SuccessfulHtmlResponse = user => "<html>" +
                                                                               "<body>" +
                                                                               $"<h4>Name: {user.Name}</h3>" +
                                                                               $"<h4>Id: {user.Id}</h4>" +
                                                                               $"<h4>Status: {user.Status.ToString()}</h3>" +
                                                                               "</body>" +
                                                                               "</html>";
    public static readonly Func<ApplicationException, string> ErrorHtmlResponse = ex => "<html>" +
                                                                                        "<body>" +
                                                                                        $"<h3>{ex.Message}</h3>" +
                                                                                        "</body>" +
                                                                                        "</html>";

    public const string SuccessfulUserRemoveMessage = "User was removed";
}
