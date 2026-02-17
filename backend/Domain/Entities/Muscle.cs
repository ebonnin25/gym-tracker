namespace backend.Domain.Entities;

public class Muscle
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<ExerciseMuscle> ExerciseMuscles { get; set; } = new List<ExerciseMuscle>();
}
