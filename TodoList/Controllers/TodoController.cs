using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Dtos;
using TodoList.Models;
using TodoList.Services;

namespace TodoList.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<TodoGetDto>>>> GetTodosByUserId()
    {
        var response = await _todoService.GetByUserId();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<TodoGetDto>>> GetTodoById([FromRoute] int id)
    {
        var response = await _todoService.GetById(id);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<TodoGetDto>>> AddTodo([FromBody] TodoCreateDto newTodo)
    {
        var response = await _todoService.Add(newTodo);

        return StatusCode((int)HttpStatusCode.Created, response);
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<TodoGetDto>>> UpdateTodo([FromBody] TodoUpdateDto updateTodo)
    {
        var response = await _todoService.Update(updateTodo);

        return Ok(response);
    }

    [HttpPatch("{id}/done")]
    public async Task<ActionResult<ServiceResponse<TodoGetDto>>> MarkTodoAsDone([FromRoute] int id)
    {
        var response = await _todoService.MarkAsDone(id);

        return Ok(response);
    }
    [HttpPatch("{id}/not-done")]
    public async Task<ActionResult<ServiceResponse<TodoGetDto>>> MarkTodoAsNotDone([FromRoute] int id)
    {
        var response = await _todoService.MarkAsNotDone(id);

        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<ServiceResponse<TodoGetDto>>> DeleteTodo([FromRoute] int id)
    {
        var response = await _todoService.Delete(id);

        return Ok(response);
    }
}