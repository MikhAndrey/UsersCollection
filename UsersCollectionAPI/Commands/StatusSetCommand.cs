using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Services.Interfaces;
using ApplicationException = UsersCollectionAPI.Model.Exceptions.ApplicationException;

namespace UsersCollectionAPI.Commands;

public class StatusSetCommand : ICommandAsync<StatusSetDto, UserResponseDto>
{
    private readonly IUserService _userService;

    public StatusSetCommand(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<UserResponseDto> ExecuteAsync(StatusSetDto dto)
    {
        try
        {
            UserRequestDto response = await _userService.SetStatusAsync(dto);
            return new()
            {
                User = response
            };
        }
        catch (ApplicationException ex)
        {
            return new()
            {
                Success = false,
                ErrorId = ex.ExceptionId,
                Message = ex.Message
            };
        }
    }
}
