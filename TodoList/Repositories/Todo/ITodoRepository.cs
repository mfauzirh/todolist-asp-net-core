using TodoList.Models;

namespace TodoList.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetByUserId(int userId);
    Task<Todo?> GetById(int id);
    Task<Todo> Add(Todo todo);
    Task<Todo> Update(Todo todo);
    Task<Todo> Delete(Todo todo);
}