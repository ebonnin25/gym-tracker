namespace backend.Domain.Entities;

public class Session
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime Date { get; set; }
    public string? Details { get; set; }
    public ICollection<SessionExercise> SessionExercises { get; set; } = new List<SessionExercise>();
}