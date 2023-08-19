using TodoList.Dtos;
using TodoList.Models;

namespace TodoList.Services;

public interface IAuthService
{
    Task<ServiceResponse<UserGetDto>> Register(UserRegisterDto newUser);
    Task<(ServiceResponse<UserGetDto>, string token)> Login(UserLoginDto loginUser);
    Task<bool> UserExists(string UserName);
}