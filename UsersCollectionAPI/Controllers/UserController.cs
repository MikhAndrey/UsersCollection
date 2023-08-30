using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersCollectionAPI.Commands;
using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Services.Interfaces;

namespace UsersCollectionAPI.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserCreateCommand _userCreateCommand;
    private readonly UserInfoCommand _userInfoCommand;
    private readonly UserRemoveCommand _userRemoveCommand;

    private readonly IUserService _userService;

    public UserController(
        UserCreateCommand userCreateCommand,
        UserInfoCommand userInfoCommand,
        UserRemoveCommand userRemoveCommand,
        IUserService userService)
    {
        _userCreateCommand = userCreateCommand;
        _userInfoCommand = userInfoCommand;
        _userRemoveCommand = userRemoveCommand;
        _userService = userService;
    }

    [HttpGet]
    [Produces("text/html")]
    [Route("Public/UserInfo")]
    public IActionResult UserInfo(int id)
    {
        string response = _userInfoCommand.Execute(id);
        return Content(response, "text/html");
    }
    
    [Authorize]
    [HttpPost]
    [Consumes("application/xml")]
    [Produces("application/xml")]
    [Route("Auth/CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateXmlRequestDto userInfo)
    {
        string response = await _userCreateCommand.ExecuteAsync(userInfo);
        return Content(response, "application/xml");
    }
    
    [Authorize]
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("Auth/RemoveUser")]
    public async Task<IActionResult> RemoveUser([FromBody] UserRemoveDto user)
    {
        UserResponseDto response = await _userRemoveCommand.ExecuteAsync(user);
        return new JsonResult(response);
    }
    
    [Authorize]
    [HttpPost]
    [Consumes("application/x-www-form-urlencoded")]
    [Produces("application/json")]
    [Route("Auth/SetStatus")]
    public async Task<IActionResult> SetStatus([FromForm] StatusSetDto dto)
    {
        UserRequestDto response = await _userService.SetStatusAsync(dto);
        return new JsonResult(response);
    }
}
