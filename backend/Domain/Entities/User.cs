namespace backend.Domain;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }

    private User() { } // EF Core

    public User(string username, string email)
    {
        Id = Guid.NewGuid();
        Username = username;
        Email = email;
    }
}
