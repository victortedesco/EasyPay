using Transactions.Infrastructure.DTOs;

namespace Transactions.Infrastructure.Abstractions;

public interface ITransactionService
{
    Task<TransactionDTO?> GetByIdAsync(Guid id);
    Task<IEnumerable<TransactionDTO>> GetBySenderIdAsync(Guid id);
    Task<IEnumerable<TransactionDTO>> GetByRecipientIdAsync(Guid id);
    Task<TransactionDTO> AddAsync(TransactionDTO transaction);
}
