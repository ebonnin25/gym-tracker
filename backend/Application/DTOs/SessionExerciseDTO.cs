namespace backend.Application.DTOs;

public class SessionExerciseDTO
{
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public decimal? Weight { get; set; }
    public int Order { get; set; }
}
