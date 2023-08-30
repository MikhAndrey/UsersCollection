using UsersCollectionAPI.Model.Entities;
using UsersCollectionAPI.Services.Interfaces;
using UsersCollectionAPI.Utils;
using ApplicationException = UsersCollectionAPI.Model.Exceptions.ApplicationException;

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
        try
        {
            User user = _userService.GetByIdAsync(userId);
            return Constants.SuccessfulHtmlResponse(user);
        }
        catch (ApplicationException ex)
        {
            return Constants.ErrorHtmlResponse(ex);
        }
    }
}
