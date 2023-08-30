using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Model.Entities;
using UsersCollectionAPI.Services.Interfaces;

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
        
        User? removedUser = await _userService.RemoveAsync(id);

        if (removedUser == null)
        {
            return new UserResponseDto
            {
                Success = false,
                ErrorId = 2,
                Message = $"User with id {id} not found"
            };
        }

        return new UserResponseDto
        { 
            Success = true, 
            Message = "User was removed", 
            User = new UserRequestDto{
                Id = removedUser.Id,
                Name = removedUser.Name,
                Status = removedUser.Status.ToString() 
            } 
        };
    }
}
