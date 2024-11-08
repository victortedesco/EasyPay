namespace Transactions.API.Application.ViewModels;

public record TransactionViewModel(Guid Id, Guid SenderId, Guid RecipientId, decimal Amount, DateTime Date);