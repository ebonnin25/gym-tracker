namespace backend.Domain.Entities;

public class ExerciseMuscle
{
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public Guid MuscleId { get; set; }
    public Muscle Muscle { get; set; } = null!;
}
