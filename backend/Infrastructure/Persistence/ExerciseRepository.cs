using backend.Domain.Entities;
using backend.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Persistence;

public class ExerciseRepository : IExerciseRepository
{
    private readonly GymContext _context;

    public ExerciseRepository(GymContext context)
    {
        _context = context;
    }

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

        return await _context.Exercises
            .Include(e => e.ExerciseMuscles)
            .ThenInclude(em => em.Muscle)
            .FirstAsync(e => e.Id == exercise.Id);
    }
}
