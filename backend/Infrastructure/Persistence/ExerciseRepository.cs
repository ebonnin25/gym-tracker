using backend.Domain.Entities;
using backend.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Persistence;

public class ExerciseRepository : IExerciseRepository
{
    private readonly GymContext _context;

    public ExerciseRepository(GymContext context) => _context = context;

    public async Task<List<Exercise>> GetAllByUserIdAsync(Guid userId)
    {
        return await _context.Exercises
            .Include(e => e.ExerciseMuscles)
            .ThenInclude(em => em.Muscle)
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    public async Task<Exercise> AddAsync(Exercise exercise)
    {
        _context.Exercises.Add(exercise);
        await _context.SaveChangesAsync();

        var created = await GetByIdWithMusclesAsync(exercise.Id);
        if (created is null)
            throw new InvalidOperationException("Exercise was not found after creation.");

        return created;
    }

    public async Task<Exercise?> GetByIdAsync(Guid exerciseId)
    {
        return await _context.Exercises.FindAsync(exerciseId);
    }

    public async Task<Exercise?> GetByIdWithMusclesAsync(Guid exerciseId)
    {
        return await _context.Exercises
            .Include(e => e.ExerciseMuscles)
            .ThenInclude(em => em.Muscle)
            .FirstOrDefaultAsync(e => e.Id == exerciseId);
    }

    public async Task<Exercise> UpdateAsync(Exercise exercise)
    {
        _context.Exercises.Update(exercise);
        await _context.SaveChangesAsync();

        var updated = await GetByIdWithMusclesAsync(exercise.Id);
        if (updated is null)
            throw new InvalidOperationException("Exercise was not found after update.");

        return updated;
    }

    public async Task DeleteAsync(Guid exerciseId)
    {
        var exercise = await GetByIdAsync(exerciseId);
        if (exercise != null)
        {
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByNameAsync(Guid userId, string name, Guid? excludeExerciseId = null)
    {
        return await _context.Exercises.AnyAsync(e =>
            e.UserId == userId &&
            e.Name == name &&
            (!excludeExerciseId.HasValue || e.Id != excludeExerciseId.Value)
        );
    }
}
