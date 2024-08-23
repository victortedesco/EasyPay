namespace Users.API.Services.Models;

public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Document { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public decimal Balance { get; private set; }
}
