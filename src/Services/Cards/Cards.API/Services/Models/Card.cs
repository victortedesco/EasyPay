using Cards.API.Infrastructure;

namespace Cards.API.Services.Models;

public class Card
{
    public int Id { get; set; }
    public required Guid UserId { get; set; }
    public required string UserName { get; set; }
    public string CardNumber { get; set; } = CardGenerator.GenerateCardNumber();
    public string SecurityNumber { get; set; } = CardGenerator.GenerateSecurityNumber();
    public DateOnly ExpireDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddYears(3));
    public decimal CardLimit { get; set; } = 1000;
    public decimal TotalExpenses { get; set; } = 0;
    public bool IsDeleted { get; set; } = false;
}
