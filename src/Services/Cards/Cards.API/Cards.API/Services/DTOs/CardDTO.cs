namespace Cards.API.Services.DTOs;

public record CardDTO(int Id, string NumberCard, string SegureNumber, DateOnly ExpireDate, string UserId, decimal CardLimite, decimal TotalExpenses);
