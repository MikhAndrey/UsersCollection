using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Model.Entities;

namespace UsersCollectionConsole;

public static class App
{
    public static string UserName;
    public static string UserPassword;

    public static async Task Run()
    {
        UserConsoleReader consoleReader = new();
        UserHttpManager httpManager = new();

        (UserName, UserPassword) = consoleReader.ReadUserCredentials();
        
        while (true)
        {
            char selectedActionChar = Menu.GetUserAction();
            
            if (selectedActionChar == MenuConstants.ExitChar)
            {
                return;
            }
            if (selectedActionChar == MenuConstants.CreateNewUserChar)
            {
                User user = consoleReader.ReadUser();
                UserCreateXmlRequestDto dto = new()
                {
                    User = new()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Status = user.Status.ToString()
                    }
                };
                await httpManager.CreateUser(dto);
            }
            if (selectedActionChar == MenuConstants.RemoveUserChar)
            {
                int userIdToRemove = consoleReader.ReadUserIdForRemoveUser();
                UserRemoveDto dto = new()
                {
                    RemoveUser = new()
                    {
                        Id = userIdToRemove
                    }
                };
                await httpManager.RemoveUser(dto);
            }
            if (selectedActionChar == MenuConstants.UserInfoChar)
            {
                int id = consoleReader.ReadUserIdForUserInfo();
                await httpManager.GetUserById(id);
            }
            if (selectedActionChar == MenuConstants.UserStatusChar)
            {
                int userId = consoleReader.ReadUserIdForSetStatus();
                Status status = consoleReader.ReadUserStatus();
                await httpManager.SetStatus(userId, status.ToString());
            }
        }
    }
}
