using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Dtos;
using TodoList.Models;
using TodoList.Services;

namespace TodoList.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}", Name = "GetUserByIdAsync")]
    [ProducesResponseType(typeof(ServiceResponse<UserGetDto>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ServiceResponse<UserGetDto>>> GetUserByIdAsync([FromRoute] int id)
    {
        ServiceResponse<UserGetDto> response = await _userService.GetByIdAsync(id);

        return Ok(response);
    }

    [HttpGet("username/{userName}")]
    [ProducesResponseType(typeof(ServiceResponse<UserGetDto>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ServiceResponse<UserGetDto>>> GetUserByUserNameAsync([FromRoute] string userName)
    {
        ServiceResponse<UserGetDto> response = await _userService.GetByUserNameAsync(userName);

        return Ok(response);
    }
}