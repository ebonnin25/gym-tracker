using backend.Domain.Entities;
using backend.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Persistence;

public class MuscleRepository : IMuscleRepository
{
    private readonly GymContext _context;
    public MuscleRepository(GymContext context) => _context = context;

    public async Task<List<Muscle>> GetAllAsync() 
        => await _context.Muscles.ToListAsync();

    public async Task<Muscle?> GetByIdAsync(Guid id) 
        => await _context.Muscles.FindAsync(id);

    public async Task<Muscle> AddAsync(Muscle muscle)
    {
        _context.Muscles.Add(muscle);
        await _context.SaveChangesAsync();
        return muscle;
    }

    public async Task<Muscle> UpdateAsync(Muscle muscle)
    {
        _context.Muscles.Update(muscle);
        await _context.SaveChangesAsync();
        return muscle;
    }

    public async Task DeleteAsync(Guid id)
    {
        var muscle = await _context.Muscles.FindAsync(id);
        if (muscle != null)
        {
            _context.Muscles.Remove(muscle);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByNameAsync(string name)
        => await _context.Muscles.AnyAsync(m => m.Name.ToLower() == name.ToLower());
}
