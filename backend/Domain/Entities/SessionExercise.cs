namespace backend.Domain.Entities;

public class SessionExercise
{
    public Guid SessionId { get; set; }
    public Session Session { get; set; } = null!;
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public decimal? Weight { get; set; }
    public int Order { get; set; }
}
