namespace backend.Domain.Entities;

public class SessionSet
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SessionId { get; set; }
    public Guid ExerciseId { get; set; }
    public SessionExercise SessionExercise { get; set; } = null!;
    public int? Reps { get; set; }
    public decimal? Weight { get; set; }
    public int Order { get; set; }
}