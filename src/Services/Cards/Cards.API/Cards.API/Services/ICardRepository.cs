using Cards.API.Infrastructure.Data;
using Cards.API.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Services;

public interface ICardRepository
{
    Task<Card?> GetByIdAsync(int id);
    Task<Card?> GetByNumberCardAsync(string numberCard);
    Task<IEnumerable<Card?>> GetByUserIdAsync(string userId);
}

public class CardRepository(ApplicationDataContext dataContext) : ICardRepository
{
    private readonly DbSet<Card> _cards = dataContext.Cards;

    public async Task<Card?> GetByIdAsync(int id)
    {
        return await _cards.FindAsync(id);
    }

    public async Task<Card?> GetByNumberCardAsync(string numberCard)
    {
        return await _cards.AsNoTracking().FirstOrDefaultAsync(u => u.NumberCard == numberCard);
    }

    public async Task<IEnumerable<Card?>> GetByUserIdAsync(string userId)
    {
        return await _cards.Where(u => u.UserId == userId).ToListAsync();
    }
}
