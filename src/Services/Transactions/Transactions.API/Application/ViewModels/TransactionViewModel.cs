namespace Transactions.API.Application.ViewModels;

public record TransactionViewModel(Guid Id, Guid SenderId, string SenderName, Guid RecipientId, string RecipientName, decimal Amount, DateTime Date);