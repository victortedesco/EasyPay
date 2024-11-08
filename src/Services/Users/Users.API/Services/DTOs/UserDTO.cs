namespace Users.API.Services.DTOs;

public record UserDTO(Guid Id, string Name, string Document, string Email, decimal Balance);
