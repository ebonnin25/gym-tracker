using backend.Application.DTOs;
using backend.Domain.Entities;
using backend.Domain.Repositories;

namespace backend.Application.Services;

public class ExerciseService
{
    private readonly IExerciseRepository _repository;
    public ExerciseService(IExerciseRepository repository) => _repository = repository;

    public async Task<List<ExerciseDTO>> GetAllByUserIdAsync(Guid userId)
    {
        var exercises = await _repository.GetAllByUserIdAsync(userId);

        return exercises.Select(e => new ExerciseDTO
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            Muscles = e.ExerciseMuscles.Select(em => new MuscleDTO
            {
                Id = em.Muscle.Id,
                Name = em.Muscle.Name
            }).ToList()
        }).ToList();
    }

    public async Task<ExerciseDTO?> GetByIdAsync(Guid userId, Guid exerciseId)
    {
        var exercise = await _repository.GetByIdWithMusclesAsync(exerciseId);
        if(exercise == null)
            return null;
        if (exercise.UserId != userId)
            throw new UnauthorizedAccessException();
        return new ExerciseDTO
        {
            Id = exercise.Id,
            Name = exercise.Name,
            Description = exercise.Description,
            Muscles = exercise.ExerciseMuscles.Select(em => new MuscleDTO
            {
                Id = em.Muscle.Id,
                Name = em.Muscle.Name
            }).ToList()
        };
    }

    public async Task<ExerciseDTO> CreateAsync(Guid userId, CreateExerciseDTO dto)
    {
        var exercise = new Exercise
        {
            UserId = userId,
            Name = dto.Name,
            Description = dto.Description
        };

        if(dto.MuscleIds == null || !dto.MuscleIds.Any())
            throw new InvalidOperationException("At least one muscle must be selected.");
        if(dto.MuscleIds.Distinct().Count() != dto.MuscleIds.Count)
            throw new InvalidOperationException("Each muscle can only be selected once.");

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
            Muscles = createdExercise.ExerciseMuscles.Select(em => new MuscleDTO
            {
                Id = em.MuscleId,
                Name = em.Muscle.Name
            }).ToList()
        };
    }

    public async Task<ExerciseDTO> UpdateAsync(Guid userId, Guid exerciseId, UpdateExerciseDTO dto)
    {
        var exercise = await _repository.GetByIdWithMusclesAsync(exerciseId)
            ?? throw new KeyNotFoundException("Exercise not found.");
        
        if (exercise.UserId != userId)
            throw new UnauthorizedAccessException();
        if(await _repository.ExistsByNameAsync(exercise.UserId, dto.Name, exerciseId))
            throw new InvalidOperationException("The name of the exercise already exists.");
        if(dto.MuscleIds == null || !dto.MuscleIds.Any())
            throw new InvalidOperationException("At least one muscle must be selected.");
        if(dto.MuscleIds.Distinct().Count() != dto.MuscleIds.Count)
            throw new InvalidOperationException("Each muscle can only be selected once..");

        exercise.Name = dto.Name;
        exercise.Description = dto.Description;

        exercise.ExerciseMuscles.Clear();
        foreach(var muscleId in dto.MuscleIds)
        {
            exercise.ExerciseMuscles.Add(new ExerciseMuscle
            {
                ExerciseId = exercise.Id,
                MuscleId = muscleId
            });
        }

        var updatedExercise = await _repository.UpdateAsync(exercise);

        return new ExerciseDTO
        {
            Id = updatedExercise.Id,
            Name = updatedExercise.Name,
            Description = updatedExercise.Description,
            Muscles = updatedExercise.ExerciseMuscles.Select(em => new MuscleDTO
            {
                Id = em.Muscle.Id,
                Name = em.Muscle.Name
            }).ToList()
        };
    }

    public async Task DeleteAsync(Guid userId, Guid exerciseId)
    {
        var exercise = await _repository.GetByIdAsync(exerciseId);
        if(exercise == null)
            throw new KeyNotFoundException("Exercise not found.");
        if (exercise.UserId != userId)
            throw new UnauthorizedAccessException();
        await _repository.DeleteAsync(exerciseId);
    }
}
