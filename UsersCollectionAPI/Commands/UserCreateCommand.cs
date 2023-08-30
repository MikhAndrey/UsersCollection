using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Model.Exceptions;
using UsersCollectionAPI.Services.Interfaces;

namespace UsersCollectionAPI.Commands;

public class UserCreateCommand : ICommandAsync<UserCreateXmlRequestDto, string>
{
    private readonly IUserService _userService;

    public UserCreateCommand(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<string> ExecuteAsync(UserCreateXmlRequestDto dto)
    {
        string response;
        try
        {
            await _userService.CreateAsync(dto.User);
            response = _userService.GenerateUserCreationSuccessXmlResponse(dto.User);
        }
        catch (UserDuplicateException ex)
        {
            response = _userService.GenerateUserCreationErrorXmlResponse(ex);
        }
        return response;
    }
}
