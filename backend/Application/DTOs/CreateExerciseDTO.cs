namespace backend.Application.DTOs;

public class CreateExerciseDTO
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<Guid> MuscleIds { get; set; } = new List<Guid>();
}
