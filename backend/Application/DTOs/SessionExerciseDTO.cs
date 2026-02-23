using backend.Domain.Entities;

namespace backend.Application.DTOs;

public class SessionExerciseDTO
{
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public List<SessionSetDTO>? Sets { get; set; }
    public int Order { get; set; }
}
