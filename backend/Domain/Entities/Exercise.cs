namespace backend.Domain.Entities;

public class Exercise
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<ExerciseMuscle> ExerciseMuscles { get; set; }
        = new List<ExerciseMuscle>();

    public User User { get; set; } = null!;
}
