using backend.Domain.Entities;

namespace backend.Domain.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
}
