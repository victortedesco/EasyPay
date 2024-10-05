using Cards.API.Application.Extensions;
using Cards.API.Services.DTOs;

namespace Cards.API.Services;

public interface ICardService
{
    Task<CardDTO?> GetByIdAsync(int id);
    Task<CardDTO?> GetByNumberCardAsync(string cardNumber);
    Task<IEnumerable<CardDTO?>> GetByUserIdAsync(string userId);
}

public class CardService(ICardRepository cardRepositoey) : ICardService
{
    private readonly ICardRepository _cardRepository = cardRepositoey;

    public async Task<CardDTO?> GetByIdAsync(int id)
    {
       var card = await _cardRepository.GetByIdAsync(id);

        return card?.ToDTO();
    }

    public async Task<CardDTO?> GetByNumberCardAsync(string cardNumber)
    {
        var card = await _cardRepository.GetByNumberCardAsync(cardNumber);

        return card?.ToDTO();
    }

    public async Task<IEnumerable<CardDTO?>> GetByUserIdAsync(string userId)
    {
        var cards = await _cardRepository.GetByUserIdAsync(userId);

        return cards.ToDTO();
    }
}
