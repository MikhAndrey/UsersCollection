namespace UsersCollectionConsole;

public static class App
{
    private static readonly UserActionManager UserActionManager = new();
    
    public static async Task RunAsync()
    {
        UserActionManager.ReadUserCredentials();
        
        while (true)
        {
            char selectedActionChar = Menu.GetUserAction();

            switch (selectedActionChar)
            {
                case MenuConstants.CreateNewUserChar:
                    await UserActionManager.CreateUserAsync();
                    break;
                case MenuConstants.RemoveUserChar:
                    await UserActionManager.RemoveUserAsync();
                    break;
                case MenuConstants.UserInfoChar:
                    await UserActionManager.GetUserInfoAsync();
                    break;
                case MenuConstants.UserStatusChar:
                    await UserActionManager.SetUserStatusAsync();
                    break;
                case MenuConstants.ExitChar:
                    return;
            }
        }
    }
}
