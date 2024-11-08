using Transactions.Domain.Models;
using Transactions.Infrastructure.DTOs;

namespace Transactions.Infrastructure.Extensions;

public static class TransactionsDTOExtensions
{
    public static IEnumerable<TransactionDTO> ToDTO(this IEnumerable<Transaction> transactions)
    {
        return transactions.Select(ToDTO);
    }

    public static TransactionDTO ToDTO(this Transaction transaction)
    {
        return new TransactionDTO(transaction.Id, transaction.SenderId, transaction.RecipientId, transaction.Amount, transaction.Date);
    }
}
