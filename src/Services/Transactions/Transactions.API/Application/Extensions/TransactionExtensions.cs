using Transactions.API.Application.ViewModels;
using Transactions.Infrastructure.DTOs;

namespace Transactions.API.Application.Extensions;

public static class TransactionExtensions
{
    public static IEnumerable<TransactionViewModel> ToViewModel(this IEnumerable<TransactionDTO> transactions)
    {
        return transactions.Select(ToViewModel);
    }

    public static TransactionViewModel ToViewModel(this TransactionDTO transaction)
    {
        return new TransactionViewModel(transaction.Id, transaction.SenderId, transaction.RecipientId, transaction.Amount, transaction.Date);
    }
}