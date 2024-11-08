namespace Transactions.Domain.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid RecipientId { get; set; }
    public decimal Amount { get; set; }
    public Guid SenderId { get; set; }
    public DateTime Date { get; set; }
}
