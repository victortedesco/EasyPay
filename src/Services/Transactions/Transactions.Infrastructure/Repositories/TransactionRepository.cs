using Microsoft.EntityFrameworkCore;
using Transactions.Domain;
using Transactions.Domain.Models;
using Transactions.Infrastructure.Data;

namespace Transactions.Infrastructure.Repositories;

public class TransactionRepository(ApplicationDbContext dataContext) : ITransactionRepository
{
    private readonly DbSet<Transaction> _transactions = dataContext.Transaction;

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        return await _transactions.FindAsync(id);
    }

    public async Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid id)
    {
        return await _transactions.OrderByDescending(t => t.Date).Where(u => u.SenderId == id || u.RecipientId == id).ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetBySenderIdAsync(Guid id)
    {
        return await _transactions.OrderByDescending(t => t.Date).Where(u => u.SenderId == id).ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByRecipientIdAsync(Guid id)
    {
        return await _transactions.OrderByDescending(t => t.Date).Where(u => u.RecipientId == id).ToListAsync();
    }

    public async Task<Transaction> AddAsync(Transaction transaction)
    {
        var entry = await _transactions.AddAsync(transaction);
        await dataContext.SaveChangesAsync();
        return entry.Entity;
    }
}
