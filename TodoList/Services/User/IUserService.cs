using TodoList.Dtos;
using TodoList.Models;

namespace TodoList.Services;

public interface IUserService
{
    Task<ServiceResponse<UserGetDto>> GetByIdAsync(int id);
    Task<ServiceResponse<UserGetDto>> GetByUserNameAsync(string userName);
}