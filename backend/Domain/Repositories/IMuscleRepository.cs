using backend.Domain.Entities;

namespace backend.Domain.Repositories;

public interface IMuscleRepository
{
    Task<List<Muscle>> GetAllAsync();
    Task<Muscle?> GetByIdAsync(Guid id);
    Task<Muscle> AddAsync(Muscle muscle);
    Task<Muscle> UpdateAsync(Muscle muscle);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsByNameAsync(string name);
}
