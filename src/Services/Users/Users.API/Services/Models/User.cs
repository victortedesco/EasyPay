namespace Users.API.Services.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Document { get; set; }
    public required string Email { get; set; }
    public decimal Balance { get; set; }
}
