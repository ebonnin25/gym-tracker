namespace backend.Application.DTOs;

public class CreateSessionDTO
{
    public DateTime Date { get; set; }
    public string? Details { get; set; }
    public List<CreateSessionExerciseDTO> Exercises { get; set; } = new List<CreateSessionExerciseDTO>();
}
