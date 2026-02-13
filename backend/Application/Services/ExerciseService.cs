using backend.Application.DTOs;
using backend.Domain.Entities;
using backend.Domain.Repositories;

namespace backend.Application.Services;

public class ExerciseService
{
    private readonly IExerciseRepository _repository;

    public ExerciseService(IExerciseRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ExerciseDTO>> GetAllByUserIdAsync(Guid userId)
    {
        var exercises = await _repository.GetAllByUserIdAsync(userId);

        return exercises.Select(e => new ExerciseDTO
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            CreatedAt = e.CreatedAt,
            Muscles = e.ExerciseMuscles.Select(em => new MuscleDTO
            {
                Id = em.Muscle.Id,
                Name = em.Muscle.Name
            }).ToList()
        }).ToList();
    }

    public async Task<ExerciseDTO> CreateAsync(Guid userId, CreateExerciseDTO dto)
    {
        var exercise = new Exercise
        {
            UserId = userId,
            Name = dto.Name,
            Description = dto.Description
        };

        foreach (var muscleId in dto.MuscleIds)
        {
            exercise.ExerciseMuscles.Add(new ExerciseMuscle
            {
                ExerciseId = exercise.Id,
                MuscleId = muscleId
            });
        }

        var createdExercise = await _repository.AddAsync(exercise);

        return new ExerciseDTO
        {
            Id = createdExercise.Id,
            Name = createdExercise.Name,
            Description = createdExercise.Description,
            CreatedAt = createdExercise.CreatedAt,
            Muscles = createdExercise.ExerciseMuscles.Select(em => new MuscleDTO
            {
                Id = em.MuscleId,
                Name = em.Muscle.Name
            }).ToList()
        };
    }
}
