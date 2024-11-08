namespace Transactions.Infrastructure.DTOs;

public record TransactionDTO(Guid Id, Guid SenderId, string SenderName, Guid RecipientId, string RecipientName, decimal Amount, DateTime Date);
