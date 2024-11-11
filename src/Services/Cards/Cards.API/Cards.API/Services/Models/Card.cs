using System.ComponentModel.DataAnnotations;

namespace Cards.API.Services.Models;

public class Card
{
    public int Id { get; set; }
    public required string NumberCard { get; set; }
    public required string SegureNumber { get; set; }
    public DateOnly ExpireDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddYears(3));
    public required string UserId { get; set; }
    public decimal CardLimite { get; set; } = 1000;
    public decimal TotalExpenses { get; set; }
}
