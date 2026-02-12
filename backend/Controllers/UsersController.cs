using Microsoft.AspNetCore.Mvc;
using backend.Application;
using backend.Application.DTOs;

namespace backend.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthService _authService;

    public UsersController(UserService userService, AuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    // GET /api/users
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    // POST /api/users/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
    {
        try
        {
            var user = await _authService.RegisterAsync(dto);
            return Ok(user); // UserDTO sans mot de passe
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    // POST /api/users/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
    {
        try
        {
            var user = await _authService.LoginAsync(dto);
            return Ok(user); // UserDTO
        }
        catch (Exception e)
        {
            return Unauthorized(new { message = e.Message });
        }
    }
}
