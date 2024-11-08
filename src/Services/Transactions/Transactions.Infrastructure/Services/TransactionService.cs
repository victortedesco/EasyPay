using Transactions.Domain;
using Transactions.Domain.Models;
using Transactions.Infrastructure.Abstractions;
using Transactions.Infrastructure.DTOs;
using Transactions.Infrastructure.Extensions;

namespace Transactions.Infrastructure.Services;

public class TransactionService(ITransactionRepository transactionRepository) : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    public async Task<TransactionDTO?> GetByIdAsync(Guid id)
    {
        var trans = await _transactionRepository.GetByIdAsync(id);
        return trans?.ToDTO();
    }

    public async Task<IEnumerable<TransactionDTO>> GetByUserIdAsync(Guid id)
    {
        var transactions = await _transactionRepository.GetByUserIdAsync(id);
        return transactions.ToDTO();
    }

    public async Task<IEnumerable<TransactionDTO>> GetBySenderIdAsync(Guid id)
    {
        var trans = await _transactionRepository.GetBySenderIdAsync(id);
        return trans.ToDTO();
    }

    public async Task<IEnumerable<TransactionDTO>> GetByRecipientIdAsync(Guid id)
    {
        var trans = await _transactionRepository.GetByRecipientIdAsync(id);
        return trans.ToDTO();
    }

    public async Task<TransactionDTO> AddAsync(TransactionDTO dto)
    {
        var model = new Transaction
        {
            SenderId = dto.SenderId,
            SenderName = dto.SenderName,
            RecipientId = dto.RecipientId,
            RecipientName = dto.RecipientName,
            Amount = dto.Amount
        };

        var transaction = await _transactionRepository.AddAsync(model);

        return transaction.ToDTO();
    }
}
