namespace Transactions.API.Application.Requests;

public class AddTransactionRequest
{
    public Guid RecipientId { get; set; }
    public required string RecipientName { get; set; }
    public decimal Amount { get; set; }
}
