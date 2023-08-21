using System.Security.Claims;
using AutoMapper;
using TodoList.Dtos;
using TodoList.Models;
using TodoList.Repositories;

namespace TodoList.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TodoService(ITodoRepository todoRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public async Task<ServiceResponse<TodoGetDto>> Add(TodoCreateDto newTodo)
    {
        var response = new ServiceResponse<TodoGetDto>();

        Todo todo = _mapper.Map<Todo>(newTodo);
        todo.UserId = GetUserId();
        todo.CreatedAt = DateTime.Now;
        todo.UpdatedAt = DateTime.Now;

        Todo createdTodo = await _todoRepository.Add(todo);

        response.Data = _mapper.Map<TodoGetDto>(createdTodo);

        return response;
    }

    public Task<ServiceResponse<TodoGetDto>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<TodoGetDto?>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<IEnumerable<TodoGetDto>>> GetByUserId()
    {
        var response = new ServiceResponse<IEnumerable<TodoGetDto>>
        {
            Data = _mapper.Map<IEnumerable<TodoGetDto>>(await _todoRepository.GetByUserId(GetUserId()))
        };

        return response;
    }

    public Task<ServiceResponse<TodoGetDto>> Update(TodoUpdateDto updateTodo)
    {
        throw new NotImplementedException();
    }
}