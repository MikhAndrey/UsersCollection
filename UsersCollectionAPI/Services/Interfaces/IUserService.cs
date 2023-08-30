using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Model.Entities;
using ApplicationException = UsersCollectionAPI.Model.Exceptions.ApplicationException;

namespace UsersCollectionAPI.Services.Interfaces;

public interface IUserService
{
    public User GetByIdAsync(int id);
    Task CreateAsync(UserRequestDto user);
    Task<User> RemoveAsync(int id);
    string GenerateUserCreationSuccessXmlResponse(UserRequestDto user);
    string GenerateUserCreationErrorXmlResponse(ApplicationException exception);
    string GenerateUserInfoHtmlResponse(User? user);
    Task<UserRequestDto> SetStatusAsync(StatusSetDto dto);
}
