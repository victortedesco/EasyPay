using Cards.API.Infrastructure.Data;
using Cards.API.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Services;

public interface ICardRepository
{
    Task<Card?> GetByIdAsync(int id);
    Task<Card?> GetByCardNumberAsync(string cardNumber);
    Task<IEnumerable<Card>> GetByUserIdAsync(Guid userId);
    Task<Card?> AddAsync(Card card);
    Task<bool> UpdateAsync(int id, Card request);
    Task<bool> DeleteAsync(int id);
}

public class CardRepository(ApplicationDbContext dataContext) : ICardRepository
{
    private readonly DbSet<Card> _cards = dataContext.Cards;

    public async Task<Card?> GetByIdAsync(int id)
    {
        return await _cards.FindAsync(id);
    }

    public async Task<Card?> GetByCardNumberAsync(string cardNumber)
    {
        return await _cards.AsNoTracking().FirstOrDefaultAsync(u => u.CardNumber == cardNumber);
    }

    public async Task<IEnumerable<Card>> GetByUserIdAsync(Guid userId)
    {
        return await _cards.AsNoTracking().Where(u => u.UserId == userId).ToListAsync();
    }

    public async Task<Card?> AddAsync(Card card)
    {
        var entity = await _cards.AddAsync(card);
        await dataContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> UpdateAsync(int id, Card request)
    {
        var card = await _cards.FindAsync(id);

        if (card is null || card.IsDeleted)
        {
            return false;
        }

        card.CardLimit = request.CardLimit;
        card.TotalExpenses = request.TotalExpenses;

        _cards.Update(card);

        return await dataContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var card = await _cards.FindAsync(id);

        if (card is null || card.IsDeleted)
        {
            return false;
        }

        card.IsDeleted = true;
        _cards.Update(card);

        return await dataContext.SaveChangesAsync() > 0;
    }
}
