using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Model.Entities;
using UsersCollectionAPI.Model.Exceptions;
using UsersCollectionAPI.Model.Infrastructure.Interfaces;
using UsersCollectionAPI.Services.Interfaces;
using UsersCollectionAPI.Utils;
using ApplicationException = UsersCollectionAPI.Model.Exceptions.ApplicationException;

namespace UsersCollectionAPI.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserCacheService _userCacheService;

    public UserService(IUnitOfWork unitOfWork, UserCacheService userCacheService)
    {
        _unitOfWork = unitOfWork;
        _userCacheService = userCacheService;
    }

    public User GetByIdAsync(int id)
    {
        User? user = _userCacheService.Get(id);
        if (user == null)
            throw new UserNotFoundException(Constants.UserNotFoundDefaultMessage(id));
        return user;
    }

    public async Task CreateAsync(UserRequestDto user)
    {
        if (_unitOfWork.Users.Exists(user.Id))
            throw new UserDuplicateException(Constants.UserDuplicateDefaultMessage(user.Id));
        
        User userToAdd = new User
        {
            Id = user.Id,
            Name = user.Name,
            Status = Enum.Parse<Status>(user.Status, true)
        };

        await _unitOfWork.Users.AddAsync(userToAdd);
        await _unitOfWork.SaveAsync();
        _userCacheService.Update(userToAdd);
    }

    public async Task<User> RemoveAsync(int id)
    {
        User? userToRemove = await _unitOfWork.Users.GetByIdAsync(id);
        if (userToRemove == null)
            throw new UserNotFoundException(Constants.UserNotFoundDefaultMessage(id));
        _unitOfWork.Users.Remove(userToRemove);
        await _unitOfWork.SaveAsync();
        _userCacheService.Remove(id);
        return userToRemove;
    }
    
    public string GenerateUserCreationSuccessXmlResponse(UserRequestDto user)
    {
        UserResponseDto userResponse = new UserResponseDto
        {
            Success = true,
            ErrorId = 0,
            User = user
        };

        string xml = new CustomXmlSerializer<UserResponseDto>().Serialize(userResponse);
        return xml;
    }

    public string GenerateUserCreationErrorXmlResponse(ApplicationException exception)
    {
        UserResponseDto userResponse = new UserResponseDto
        {
            Success = false,
            ErrorId = exception.ExceptionId,
            Message = exception.Message
        };

        string xml = new CustomXmlSerializer<UserResponseDto>().Serialize(userResponse);
        return xml;
    }

    public string GenerateUserInfoHtmlResponse(User? user)
    {
        if (user == null)
            return "<html>" +
                   "<body>" +
                   "<h3>User not found</h3>" +
                   "</body>" +
                   "</html>";
        return "<html>" +
               "<body>" +
               $"<h4>Name: {user.Name}</h3>" +
               $"<h4>Id: {user.Id}</h4>" +
               $"<h4>Status: {user.Status.ToString()}</h3>" +
               "</body>" +
               "</html>";
    }

    public async Task<UserRequestDto> SetStatusAsync(StatusSetDto dto)
    {
        User? user = await _unitOfWork.Users.GetByIdAsync(int.Parse(dto.Id));
        if (user == null)
        {
            //Todo: Handle error here
        }
        else
        {
            user.Status = (Status)Enum.Parse(typeof(Status), dto.NewStatus);
            await _unitOfWork.SaveAsync();
        }

        return new UserRequestDto
        {
            Id = user.Id,
            Name = user.Name,
            Status = dto.NewStatus
        };
    }
}
