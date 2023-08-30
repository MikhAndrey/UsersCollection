using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Model.Entities;
using UsersCollectionAPI.Model.Exceptions;
using UsersCollectionAPI.Model.Infrastructure.Interfaces;
using UsersCollectionAPI.Services.Interfaces;
using UsersCollectionAPI.Utils;

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

    public async Task<UserRequestDto> SetStatusAsync(StatusSetDto dto)
    {
        User? user = await _unitOfWork.Users.GetByIdAsync(dto.Id);
        if (user == null)
            throw new UserNotFoundException(Constants.UserNotFoundDefaultMessage(dto.Id));
        
        user.Status = (Status)Enum.Parse(typeof(Status), dto.NewStatus);
        await _unitOfWork.SaveAsync();
       
        return new UserRequestDto
        {
            Id = user.Id,
            Name = user.Name,
            Status = dto.NewStatus
        };
    }
}
