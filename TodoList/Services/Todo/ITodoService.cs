using TodoList.Dtos;
using TodoList.Models;

namespace TodoList.Services;

public interface ITodoService
{
    Task<ServiceResponse<IEnumerable<TodoGetDto>>> GetByUserId();
    Task<ServiceResponse<TodoGetDto>> GetById(int id);
    Task<ServiceResponse<TodoGetDto>> Add(TodoCreateDto newTodo);
    Task<ServiceResponse<TodoGetDto>> Update(TodoUpdateDto updateTodo);
    Task<ServiceResponse<TodoGetDto>> Delete(int id);
}