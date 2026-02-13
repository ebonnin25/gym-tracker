using backend.Domain.Entities;

namespace backend.Domain.Repositories;

public interface IExerciseRepository
{
    Task<List<Exercise>> GetAllByUserIdAsync(Guid userId);
    Task<Exercise> AddAsync(Exercise exercise);
}
