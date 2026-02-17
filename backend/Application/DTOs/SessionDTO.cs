namespace backend.Application.DTOs;

public class SessionDTO
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string? Details { get; set; }
    public List<SessionExerciseDTO> Exercises { get; set; } = new List<SessionExerciseDTO>();
}
