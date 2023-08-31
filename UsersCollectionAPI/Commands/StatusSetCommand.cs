using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Services.Interfaces;
using ApplicationException = UsersCollectionAPI.Model.Exceptions.ApplicationException;

namespace UsersCollectionAPI.Commands;

public class StatusSetCommand : ICommandAsync<StatusSetDto, UserRequestDto?>
{
    private readonly IUserService _userService;

    public StatusSetCommand(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<UserRequestDto?> ExecuteAsync(StatusSetDto dto)
    {
        try
        {
            UserRequestDto response = await _userService.SetStatusAsync(dto);
            return response;
        }
        catch (ApplicationException)
        {
            return null;
        }
    }
}
