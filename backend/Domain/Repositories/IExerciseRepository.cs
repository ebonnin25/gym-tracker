using backend.Domain.Entities;

namespace backend.Domain.Repositories;

public interface IExerciseRepository
{
    Task<List<Exercise>> GetAllByUserIdAsync(Guid userId);
    Task<Exercise> AddAsync(Exercise exercise);
    Task<Exercise?> GetByIdAsync(Guid exerciseId);
    Task<Exercise?> GetByIdWithMusclesAsync(Guid exerciseId);
    Task<Exercise> UpdateAsync(Exercise exercise);
    Task DeleteAsync(Guid exerciseId);
    Task<bool> ExistsByNameAsync(Guid userId, string name, Guid? excludeExerciseId = null);
}
