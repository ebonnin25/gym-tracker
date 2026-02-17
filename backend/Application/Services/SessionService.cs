using backend.Application.DTOs;
using backend.Domain.Entities;
using backend.Domain.Repositories;

namespace backend.Application.Services;

public class SessionService
{
    private readonly ISessionRepository _repository;

    public SessionService(ISessionRepository repository) => _repository = repository;

    public async Task<List<SessionDTO>> GetAllByUserIdAsync(Guid userId)
    {
        var sessions = await _repository.GetAllByUserIdAsync(userId);

        return sessions.Select(MapToDTO).ToList();
    }

    public async Task<SessionDTO> GetByIdAsync(Guid userId, Guid sessionId)
    {
        var session = await _repository.GetByIdWithExercisesAsync(sessionId)
            ?? throw new KeyNotFoundException("Session not found.");

        if (session.UserId != userId)
            throw new UnauthorizedAccessException();
            
        return MapToDTO(session);
    }

    public async Task<SessionDTO> CreateAsync(Guid userId, CreateSessionDTO dto)
    {
        if (dto.Exercises == null || !dto.Exercises.Any())
            throw new InvalidOperationException("A session must contain at least one exercise.");

        var session = new Session
        {
            UserId = userId,
            Date = dto.Date,
            Details = dto.Details
        };

        foreach (var ex in dto.Exercises)
        {
            session.SessionExercises.Add(new SessionExercise
            {
                ExerciseId = ex.ExerciseId,
                Sets = ex.Sets,
                Reps = ex.Reps,
                Weight = ex.Weight,
                Order = ex.Order
            });
        }

        var created = await _repository.AddAsync(session);

        return MapToDTO(created);
    }

    public async Task<SessionDTO> UpdateAsync(Guid userId, Guid sessionId, UpdateSessionDTO dto)
    {
        var session = await _repository.GetByIdWithExercisesAsync(sessionId)
            ?? throw new KeyNotFoundException("Session not found.");

        if (session.UserId != userId)
            throw new UnauthorizedAccessException();

        session.Date = dto.Date;
        session.Details = dto.Details;
        session.SessionExercises.Clear();

        foreach (var ex in dto.Exercises)
        {
            session.SessionExercises.Add(new SessionExercise
            {
                ExerciseId = ex.ExerciseId,
                Sets = ex.Sets,
                Reps = ex.Reps,
                Weight = ex.Weight,
                Order = ex.Order
            });
        }

        var updated = await _repository.UpdateAsync(session);

        return MapToDTO(updated);
    }

    public async Task DeleteAsync(Guid userId, Guid sessionId)
    {
        var session = await _repository.GetByIdAsync(sessionId);
        if (session == null)
            throw new KeyNotFoundException("Session not found.");

        await _repository.DeleteAsync(sessionId);
    }

    private static SessionDTO MapToDTO(Session session)
    {
        return new SessionDTO
        {
            Id = session.Id,
            Date = session.Date,
            Details = session.Details,
            Exercises = session.SessionExercises
                .OrderBy(se => se.Order)
                .Select(se => new SessionExerciseDTO
                {
                    ExerciseId = se.ExerciseId,
                    ExerciseName = se.Exercise.Name,
                    Sets = se.Sets,
                    Reps = se.Reps,
                    Weight = se.Weight,
                    Order = se.Order
                }).ToList()
        };
    }
}
