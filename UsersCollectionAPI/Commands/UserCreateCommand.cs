using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Services.Interfaces;
using UsersCollectionAPI.Utils;
using ApplicationException = UsersCollectionAPI.Model.Exceptions.ApplicationException;

namespace UsersCollectionAPI.Commands;

public class UserCreateCommand : ICommandAsync<UserCreateXmlRequestDto, string>
{
    private readonly IUserService _userService;
    
    private const int OkXmlResponseErrorId = 0;

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
            UserResponseDto userResponse = new UserResponseDto
            {
                Success = true,
                ErrorId = OkXmlResponseErrorId,
                User = dto.User
            };

            response = new CustomXmlSerializer<UserResponseDto>().Serialize(userResponse);
        }
        catch (ApplicationException ex)
        {
            UserResponseDto userResponse = new UserResponseDto
            {
                Success = false,
                ErrorId = ex.ExceptionId,
                Message = ex.Message
            };

            response = new CustomXmlSerializer<UserResponseDto>().Serialize(userResponse);
        }
        
        return response;
    }
}
