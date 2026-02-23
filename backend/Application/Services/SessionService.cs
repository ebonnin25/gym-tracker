using System.Diagnostics.Tracing;
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
        var session = new Session
        {
            UserId = userId,
            Date = dto.Date,
            Details = dto.Details
        };

        if (dto.Exercises != null) foreach (var ex in dto.Exercises)
        {
            var sessionExercise = new SessionExercise
            {
                ExerciseId = ex.ExerciseId,
                Order = ex.Order
            };

            if (ex.Sets != null) foreach (var set in ex.Sets)
            {
                sessionExercise.Sets.Add(new SessionSet
                {
                    Reps = set.Reps,
                    Weight = set.Weight,
                    Order = set.Order
                });
            }

            session.SessionExercises.Add(sessionExercise);
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

        if (dto.Exercises != null) foreach (var ex in dto.Exercises)
        {
            var sessionExercise = new SessionExercise
            {
                ExerciseId = ex.ExerciseId,
                Order = ex.Order
            };

            if (ex.Sets != null) foreach (var set in ex.Sets)
            {
                sessionExercise.Sets.Add(new SessionSet
                {
                    Reps = set.Reps,
                    Weight = set.Weight,
                    Order = set.Order
                });
            }
            
            session.SessionExercises.Add(sessionExercise);
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
                    Order = se.Order,
                    Sets = se.Sets
                        .OrderBy(s => s.Order)
                        .Select(s => new SessionSetDTO
                        {
                            Reps = s.Reps,
                            Weight = s.Weight,
                            Order = s.Order
                        }).ToList()
                }).ToList()
        };
    }
}
