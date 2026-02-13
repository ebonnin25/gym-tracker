using backend.Domain.Entities;
using backend.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Persistence;

public class MuscleRepository : IMuscleRepository
{
    private readonly GymContext _context;

    public MuscleRepository(GymContext context)
    {
        _context = context;
    }

    public async Task<List<Muscle>> GetAllAsync()
    {
        return await _context.Muscles.ToListAsync();
    }

    public async Task AddAsync(Muscle muscle)
    {
        _context.Muscles.Add(muscle);
        await _context.SaveChangesAsync();
    }
}
