using backend.Domain.Entities;

namespace backend.Application.DTOs;

public class CreateSessionExerciseDTO
{
    public Guid ExerciseId { get; set; }
    public int Order { get; set; }
    public List<SessionSetDTO>? Sets { get; set; }
}
