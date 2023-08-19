using Microsoft.AspNetCore.Mvc;
using TodoList.Dtos;
using TodoList.Models;
using TodoList.Services;

namespace TodoList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<UserGetDto>>> Login([FromBody] UserLoginDto loginUser)
    {
        var (response, token) = await _authService.Login(loginUser);

        if (token is not null)
        {
            Response.Headers.Add("Authorization", $"Bearer {token}");
        }

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<UserGetDto>>> Register([FromBody] UserRegisterDto newUser)
    {
        ServiceResponse<UserGetDto> response = await _authService.Register(newUser);

        return StatusCode((int)response.StatusCode, response);
    }
}