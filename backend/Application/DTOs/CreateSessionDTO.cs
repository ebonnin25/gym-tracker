using System.ComponentModel.DataAnnotations;

namespace backend.Application.DTOs;

public class CreateSessionDTO
{
    [Required]
    public DateTime Date { get; set; }
    public string? Details { get; set; }
    [Required]
    [MinLength(1, ErrorMessage = "At least one exercise must be selected.")]
    public List<CreateSessionExerciseDTO>? Exercises { get; set; }
}
