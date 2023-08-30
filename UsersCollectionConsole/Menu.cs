namespace UsersCollectionConsole;

public static class Menu
{
    public static char GetUserAction()
    {
        Console.WriteLine(MenuConstants.ActionSelectMessage);
        Console.WriteLine($"{MenuConstants.CreateNewUserChar} - Create a new user\n" +
                          $"{MenuConstants.RemoveUserChar} - Remove user\n" +
                          $"{MenuConstants.UserInfoChar} - Get user info\n" +
                          $"{MenuConstants.UserStatusChar} - Set user status\n" +
                          $"{MenuConstants.ExitChar} - Exit");
        char selectedActionNumber = Console.ReadKey().KeyChar;
        Console.ReadLine();
        return selectedActionNumber;
    }
}
