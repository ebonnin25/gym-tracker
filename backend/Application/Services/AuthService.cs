using backend.Domain.Repositories;
using backend.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Application.Services;

public class AuthService
{
    private readonly UserService _userService;
    private readonly IUserRepository _userRepository;

    public AuthService(UserService userService, IUserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Register an user
    /// </summary>
    /// <param name="dto">RegisterUserDTO with username, email, password</param>
    /// <returns>UserDTO without password</returns>
    public async Task<UserDTO> RegisterAsync(RegisterUserDTO dto)
    {
        var existingUsers = await _userRepository.GetAllAsync();
        if (existingUsers.Any(u => u.Email == dto.Email))
            throw new Exception("Email already used");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = await _userService.CreateUserAsync(dto.Username, dto.Email, passwordHash);

        return user;
    }

    /// <summary>
    /// Connect an user
    /// </summary>
    /// <param name="dto">LoginUserDTO with email et password</param>
    /// <returns>UserDTO if valid credentials</returns>
    public async Task<UserDTO> LoginAsync(LoginUserDTO dto)
    {
        var users = await _userRepository.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Email == dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        return new UserDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }

    public async Task<AuthResponseDTO> LoginAsync(LoginUserDTO dto, string jwtKey, string issuer, string audience)
    {
        var users = await _userRepository.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Email == dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new AuthResponseDTO
        {
            User = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            },
            Token = tokenString
        };
    }
}
