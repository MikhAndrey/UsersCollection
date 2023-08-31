using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Model.Entities;

namespace UsersCollectionConsole;

public class UserActionManager
{
    private readonly UserConsoleReader _consoleReader = new();
    private readonly UserHttpManager _httpManager = new();
    
    public static string UserName;
    public static string UserPassword;
    
    public async Task CreateUserAsync()
    {
        User user = _consoleReader.ReadUser();
        UserCreateXmlRequestDto dto = new()
        {
            User = new()
            {
                Id = user.Id,
                Name = user.Name,
                Status = user.Status.ToString()
            }
        };
        UserResponseDto response = await _httpManager.CreateUserAsync(dto);
        if (!response.Success)
            Console.WriteLine($"Error: {response.Message}");
    }

    public async Task RemoveUserAsync()
    {
        int userIdToRemove = _consoleReader.ReadUserIdForRemoveUser();
        UserRemoveDto dto = new()
        {
            RemoveUser = new()
            {
                Id = userIdToRemove
            }
        };
        UserResponseDto response = await _httpManager.RemoveUserAsync(dto);
        Console.WriteLine(response.Message);
    }

    public async Task GetUserInfoAsync()
    {
        int id = _consoleReader.ReadUserIdForUserInfo();
        await _httpManager.GetUserByIdAsync(id);
    }

    public async Task SetUserStatusAsync()
    {
        int userId = _consoleReader.ReadUserIdForSetStatus();
        Status status = _consoleReader.ReadUserStatus();
        await _httpManager.SetStatusAsync(userId, status.ToString());
    }

    public void ReadUserCredentials()
    {
        (UserName, UserPassword) = _consoleReader.ReadUserCredentials();
    }
}
