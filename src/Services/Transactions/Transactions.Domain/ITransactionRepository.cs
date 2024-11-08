using Transactions.Domain.Models;

namespace Transactions.Domain;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id);
    Task<IEnumerable<Transaction>> GetBySenderIdAsync(Guid id);
    Task<IEnumerable<Transaction>> GetByRecipientIdAsync(Guid id);
    Task<Transaction> AddAsync(Transaction transaction);
}
