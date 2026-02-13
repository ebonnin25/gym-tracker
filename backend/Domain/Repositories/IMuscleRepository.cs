using backend.Domain.Entities;

namespace backend.Domain.Repositories;

public interface IMuscleRepository
{
    Task<List<Muscle>> GetAllAsync();
    Task AddAsync(Muscle muscle);
}
