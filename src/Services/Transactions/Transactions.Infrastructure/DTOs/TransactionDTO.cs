namespace Transactions.Infrastructure.DTOs;

public record TransactionDTO(Guid Id, Guid SenderId, Guid RecipientId, decimal Amount, DateTime Date);
