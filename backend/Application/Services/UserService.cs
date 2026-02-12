using backend.Domain;

namespace backend.Application;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        var users = await _repository.GetAllAsync();

        return users.Select(u => new UserDTO
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email
        }).ToList();
    }

    public async Task<UserDTO> CreateUserAsync(string username, string email, string passwordHash)
    {
        var user = new User(username, email, passwordHash);
        await _repository.AddAsync(user);

        return new UserDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }
}
