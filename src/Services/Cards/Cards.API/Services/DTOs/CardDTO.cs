namespace Cards.API.Services.DTOs;

public record CardDTO(int Id, Guid UserId, string UserName, string CardNumber, string SecurityNumber, DateOnly ExpireDate, decimal CardLimit, decimal TotalExpenses);
