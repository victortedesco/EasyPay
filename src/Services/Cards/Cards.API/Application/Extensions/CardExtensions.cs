using Cards.API.Application.ViewModels;
using Cards.API.Services.DTOs;
using Cards.API.Services.Models;

namespace Cards.API.Application.Extensions;

public static class CardExtensions
{
    public static IEnumerable<CardViewModel> ToViewModel(this IEnumerable<CardDTO> cards)
    {
        return cards.Select(ToViewModel);
    }

    public static CardViewModel ToViewModel(this CardDTO cardDTO)
    {
        return new CardViewModel(cardDTO.Id, cardDTO.UserId, cardDTO.UserName, cardDTO.CardNumber, cardDTO.SecurityNumber, cardDTO.ExpireDate, cardDTO.CardLimit, cardDTO.TotalExpenses);
    }


    public static IEnumerable<CardDTO> ToDTO(this IEnumerable<Card> cards)
    {
        return cards.Select(ToDTO);
    }

    public static CardDTO ToDTO(this Card card)
    {
        return new CardDTO(card.Id, card.UserId, card.UserName, card.CardNumber, card.SecurityNumber, card.ExpireDate, card.CardLimit, card.TotalExpenses);
    }
}
