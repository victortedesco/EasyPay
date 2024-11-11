using Cards.API.Application.ViewModels;
using Cards.API.Services.DTOs;
using Cards.API.Services.Models;
using System.Collections;

namespace Cards.API.Application.Extensions;

public static class CardExtensions
{
    public static IEnumerable<CardViewModel> ToViewModel(this IEnumerable<CardDTO> cards)
    { 
        return cards.Select(ToViewModel);
    }

    public static CardViewModel ToViewModel(this CardDTO cardDTO)
    {
        return new CardViewModel(cardDTO.Id, cardDTO.NumberCard, cardDTO.SegureNumber, cardDTO.ExpireDate, cardDTO.UserId, cardDTO.CardLimite, cardDTO.TotalExpenses);
    }


    public static IEnumerable<CardDTO> ToDTO(this IEnumerable<Card> cards)
    {
        return cards.Select(ToDTO);
    }

    public static CardDTO ToDTO(this Card card)
    {
        return new CardDTO(card.Id, card.NumberCard, card.SegureNumber, card.ExpireDate, card.UserId, card.CardLimite, card.TotalExpenses);
    }
}
