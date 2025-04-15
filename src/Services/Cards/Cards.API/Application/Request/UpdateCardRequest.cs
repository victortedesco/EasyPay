namespace Cards.API.Application.Request;

public record UpdateCardRequest(decimal CardLimit, decimal TotalExpenses);