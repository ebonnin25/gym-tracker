using Microsoft.AspNetCore.Mvc;
using backend.Application.Services;
using backend.Application.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : BaseApiController
{
    private readonly UserService _userService;
    private readonly AuthService _authService;
    private readonly IConfiguration _configuration;

    public UsersController(UserService userService, AuthService authService, IConfiguration configuration)
    {
        _userService = userService;
        _authService = authService;
        _configuration = configuration;
    }

    // GET /api/users
    [HttpGet]
    [Authorize]
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
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = jwtSettings.GetValue<string>("Key") 
                    ?? throw new Exception("JWT Key is missing in configuration");
            var issuer = jwtSettings.GetValue<string>("Issuer") 
                    ?? throw new Exception("JWT Issuer is missing");
            var audience = jwtSettings.GetValue<string>("Audience") 
                    ?? throw new Exception("JWT Audience is missing");

            var response = await _authService.LoginAsync(dto, key, issuer, audience);
            return Ok(response);
        }
        catch (Exception e)
        {
            return Unauthorized(new { message = e.Message });
        }
    }

    // GET /api/users/me
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();
        var user = await _userService.GetUserByIdAsync(Guid.Parse(userIdClaim.Value));
        return Ok(user);
    }
}
