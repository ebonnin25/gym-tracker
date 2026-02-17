using backend.Domain.Entities;
using backend.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Persistence;

public class SessionRepository : ISessionRepository
{
    private readonly GymContext _context;

    public SessionRepository(GymContext context)
    {
        _context = context;
    }

    public async Task<List<Session>> GetAllByUserIdAsync(Guid userId)
    {
        return await _context.Sessions
            .Where(s => s.UserId == userId)
            .Include(s => s.SessionExercises)
                .ThenInclude(se => se.Exercise)
            .ToListAsync();
    }

    public async Task<Session?> GetByIdWithExercisesAsync(Guid sessionId)
    {
        return await _context.Sessions
            .Include(s => s.SessionExercises)
                .ThenInclude(se => se.Exercise)
            .FirstOrDefaultAsync(s => s.Id == sessionId);
    }

    public async Task<Session?> GetByIdAsync(Guid sessionId)
    {
        return await _context.Sessions
            .FirstOrDefaultAsync(s => s.Id == sessionId);
    }

    public async Task<Session> AddAsync(Session session)
    {
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        return (await GetByIdWithExercisesAsync(session.Id))!;
    }

    public async Task<Session> UpdateAsync(Session session)
    {
        _context.Sessions.Update(session);
        await _context.SaveChangesAsync();

        return (await GetByIdWithExercisesAsync(session.Id))!;
    }

    public async Task DeleteAsync(Guid sessionId)
    {
        var session = await _context.Sessions.FindAsync(sessionId);
        if (session != null)
        {
            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
        }
    }
}
