using backend.Domain.Entities;

namespace backend.Domain.Repositories;

public interface ISessionRepository
{
    Task<List<Session>> GetAllByUserIdAsync(Guid userId);
    Task<Session?> GetByIdWithExercisesAsync(Guid sessionId);
    Task<Session?> GetByIdAsync(Guid sessionId);
    Task<Session> AddAsync(Session session);
    Task<Session> UpdateAsync(Session session);
    Task DeleteAsync(Guid sessionId);
}
