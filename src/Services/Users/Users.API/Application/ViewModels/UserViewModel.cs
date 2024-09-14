namespace Users.API.Application.ViewModels;

public record UserViewModel(int Id, string Name, string Document, string Email, DateOnly BirthDate, decimal Balance);
