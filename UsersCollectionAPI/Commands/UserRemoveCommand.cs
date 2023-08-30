using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Model.Entities;
using UsersCollectionAPI.Services.Interfaces;
using UsersCollectionAPI.Utils;
using ApplicationException = UsersCollectionAPI.Model.Exceptions.ApplicationException;

namespace UsersCollectionAPI.Commands;

public class UserRemoveCommand : ICommandAsync<UserRemoveDto, UserResponseDto>
{
    private readonly IUserService _userService;

    public UserRemoveCommand(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<UserResponseDto> ExecuteAsync(UserRemoveDto dto)
    {
        int id = dto.RemoveUser.Id;

        try
        {
            User removedUser = await _userService.RemoveAsync(id);
            
            return new UserResponseDto
            { 
                Success = true, 
                Message = Constants.SuccessfulUserRemoveMessage, 
                User = new UserRequestDto{
                    Id = removedUser.Id,
                    Name = removedUser.Name,
                    Status = removedUser.Status.ToString() 
                } 
            };
        }
        catch (ApplicationException ex)
        {
            return new UserResponseDto
            {
                Success = false,
                ErrorId = ex.ExceptionId,
                Message = ex.Message
            };
        }
    }
}
