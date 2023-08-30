using UsersCollectionAPI.Model.Entities;
using UsersCollectionAPI.Services.Interfaces;

namespace UsersCollectionAPI.Commands;

public class UserInfoCommand : ICommand<int, string>
{
    private readonly IUserService _userService;

    public UserInfoCommand(IUserService userService)
    {
        _userService = userService;
    }
        
    public string Execute(int userId)
    {
        User? user = _userService.GetByIdAsync(userId);
        return _userService.GenerateUserInfoHtmlResponse(user);
    }
}
