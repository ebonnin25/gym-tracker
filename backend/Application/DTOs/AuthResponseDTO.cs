namespace backend.Application.DTOs;

public class AuthResponseDTO
{
    public UserDTO User { get; set; } = default!;
    public string Token { get; set; } = string.Empty;
}
