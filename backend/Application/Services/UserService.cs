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

    public async Task CreateUserAsync(string username, string email)
    {
        var user = new User(username, email);
        await _repository.AddAsync(user);
    }
}
