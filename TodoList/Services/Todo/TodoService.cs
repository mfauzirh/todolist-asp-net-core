using System.Security.Claims;
using AutoMapper;
using TodoList.Dtos;
using TodoList.Models;
using TodoList.Repositories;
using TodoList.Exceptions;

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

    public async Task<ServiceResponse<TodoGetDto>> Delete(int id)
    {
        var response = new ServiceResponse<TodoGetDto>();

        Todo? todo = await _todoRepository.GetById(id);

        if (todo is null)
        {
            throw new NotFoundException($"Todo with id '{id}' is not exist.");
        }

        Todo deletedTodo = await _todoRepository.Delete(todo);

        response.Data = _mapper.Map<TodoGetDto>(deletedTodo);

        return response;
    }

    public async Task<ServiceResponse<TodoGetDto>> GetById(int id)
    {
        var response = new ServiceResponse<TodoGetDto>();

        Todo? todo = await _todoRepository.GetById(id);

        if (todo is null)
        {
            throw new NotFoundException($"Todo with id '{id}' is not exist.");
        }

        response.Data = _mapper.Map<TodoGetDto>(todo);

        return response;
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