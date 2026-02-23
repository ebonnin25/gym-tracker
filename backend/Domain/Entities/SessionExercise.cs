namespace backend.Domain.Entities;

public class SessionExercise
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public Session Session { get; set; } = null!;
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public int Order { get; set; }
    public ICollection<SessionSet> Sets { get; set; } = new List<SessionSet>();
}
