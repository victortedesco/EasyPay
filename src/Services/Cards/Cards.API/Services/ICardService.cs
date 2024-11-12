using Cards.API.Application.Extensions;
using Cards.API.Services.DTOs;
using Cards.API.Services.Models;

namespace Cards.API.Services;

public interface ICardService
{
    Task<CardDTO?> GetByIdAsync(int id);
    Task<CardDTO?> GetByCardNumberAsync(string cardNumber);
    Task<IEnumerable<CardDTO>> GetByUserIdAsync(Guid userId);
    Task<CardDTO?> AddAsync(Guid userId, string UserName);
    Task<bool> UpdateAsync(int id, CardDTO request);
    Task<bool> DeleteAsync(int id);
}

public class CardService(ICardRepository cardRepository) : ICardService
{
    private readonly ICardRepository _cardRepository = cardRepository;

    public async Task<CardDTO?> GetByIdAsync(int id)
    {
        var card = await _cardRepository.GetByIdAsync(id);

        return card?.ToDTO();
    }

    public async Task<CardDTO?> GetByCardNumberAsync(string cardNumber)
    {
        var card = await _cardRepository.GetByCardNumberAsync(cardNumber);

        return card?.ToDTO();
    }

    public async Task<IEnumerable<CardDTO>> GetByUserIdAsync(Guid userId)
    {
        var cards = await _cardRepository.GetByUserIdAsync(userId);

        return cards.ToDTO();
    }

    public async Task<CardDTO?> AddAsync(Guid userId, string userName)
    {
        var card = new Card()
        {
            UserId = userId,
            UserName = userName
        };

        var result = await _cardRepository.AddAsync(card);

        return result?.ToDTO();
    }

    public async Task<bool> UpdateAsync(int id, CardDTO request)
    {
        var model = new Card()
        {
            UserId = Guid.Empty,
            UserName = "",
            CardLimit = request.CardLimit,
            TotalExpenses = request.TotalExpenses
        };

        return await _cardRepository.UpdateAsync(id, model);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _cardRepository.DeleteAsync(id);
    }
}
