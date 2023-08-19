using TodoList.Models;

namespace TodoList.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUserNameAsync(string userName);
    Task<User> AddAsync(User user);
}