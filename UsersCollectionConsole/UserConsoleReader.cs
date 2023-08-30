using UsersCollectionAPI.Model.Entities;

namespace UsersCollectionConsole;

public class UserConsoleReader
{
    private const string EnterUserIdStandardMessage = "Enter user id: ";
    private const string EnterUserNameStandardMessage = "Enter user name: ";
    private const string EnterUserIdWhenRemoveUserMessage = "Enter id of user that you want to remove: ";
    private const string EnterUserIdWhenGetUserInfoMessage = "Enter id of user whose info you want to get: ";
    private const string EnterUserIdWhenChangeUserStatusMessage = "Enter id of user whose status you want to change: ";
    private const string UserStatusChooseMenuMessage = "Enter a number describing the user's status:\n" +
                                                       "1 - New\n" +
                                                       "2 - Active\n" +
                                                       "3 - Blocked\n" +
                                                       "4 - Deleted";
    private const string EnterCurrentUserNameMessage = "Enter your name: ";
    private const string EnterCurrentUserPasswordMessage = "Enter your password: ";
    
    private const string NonIntegerUserIdWarningMessage = "User id must be an integer";
    private const string EmptyUserNameWarningMessage = "User name must be a non-empty string and shouldn't include only whitespaces";
    private const string WrongUserStatusWarningMessage = "Please, enter a number describing the user's status";
    private const string EmptyCurrentUserNameWarningMessage =
        "Your name must be a non-empty string and shouldn't include only whitespaces";
    private const string EmptyCurrentUserPasswordWarningMessage =
        "Your name must be a non-empty string and shouldn't include only whitespaces";
    
    public User ReadUser()
    {
        int userId = ReadUserId(EnterUserIdStandardMessage);
        string userName = ReadUserName();
        Status status = ReadUserStatus();

        User user = new()
        {
            Id = userId,
            Name = userName,
            Status = status
        };

        return user;
    }

    public int ReadUserIdForRemoveUser()
    {
        int id = ReadUserId(EnterUserIdWhenRemoveUserMessage);
        return id;
    }
    
    public int ReadUserIdForUserInfo()
    {
        int id = ReadUserId(EnterUserIdWhenGetUserInfoMessage);
        return id;
    }
    
    public int ReadUserIdForSetStatus()
    {
        int id = ReadUserId(EnterUserIdWhenChangeUserStatusMessage);
        return id;
    }

    public Status ReadUserStatus()
    {
        while (true)
        {
            Console.WriteLine(UserStatusChooseMenuMessage);
            string? potentialUserStatus = Console.ReadLine()?.Trim();
            if (!int.TryParse(potentialUserStatus, out int userStatus) ||
                !Enum.IsDefined(typeof(Status), userStatus))
            {
                Console.WriteLine(WrongUserStatusWarningMessage);
                continue;
            }
            
            return (Status) userStatus;
        }
    }

    public (string userName, string password) ReadUserCredentials()
    {
        string userName = ReadNonEmptyString(EnterCurrentUserNameMessage, EmptyCurrentUserNameWarningMessage);
        string password = ReadNonEmptyString(EnterCurrentUserPasswordMessage, EmptyCurrentUserPasswordWarningMessage);
        return (userName, password);
    }

    private string ReadNonEmptyString(string informationMessage, string warningMessage)
    {
        while (true)
        {
            Console.Write(informationMessage);
            string? input = Console.ReadLine()?.Trim();
            if (String.IsNullOrEmpty(input))
            {
                Console.WriteLine(warningMessage);
                continue;
            }

            return input;
        }
    }
    
    private string ReadUserName()
    {
        string userName = ReadNonEmptyString(EnterUserNameStandardMessage, EmptyUserNameWarningMessage);
        return userName;
    }

    private int ReadUserId(string consoleMessage)
    {
        while (true)
        {
            Console.Write(consoleMessage);
            string? potentialUserId = Console.ReadLine()?.Trim();
            if (!int.TryParse(potentialUserId, out int userId))
            {
                Console.WriteLine(NonIntegerUserIdWarningMessage);
                continue;
            }

            return userId;
        }
    }
}
