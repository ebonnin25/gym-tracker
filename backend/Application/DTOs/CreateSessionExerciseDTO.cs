namespace backend.Application.DTOs;

public class CreateSessionExerciseDTO
{
    public Guid ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public decimal? Weight { get; set; }
    public int Order { get; set; }
}
