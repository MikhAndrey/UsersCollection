using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Model.Entities;

namespace UsersCollectionAPI.Services.Interfaces;

public interface IUserService
{
    public User GetById(int id);
    Task CreateAsync(UserRequestDto user);
    Task<User> RemoveAsync(int id);
    Task<UserRequestDto> SetStatusAsync(StatusSetDto dto);
}
