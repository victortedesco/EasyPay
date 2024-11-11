using Cards.API.Application.Extensions;
using Cards.API.Application.ViewModels;
using Cards.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cards.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardController(ICardService cardService) : ControllerBase
{
    private readonly ICardService _cardService = cardService;

    [HttpGet("id/{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CardViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var card = await _cardService.GetByIdAsync(id);

        if (card is null)
            return NotFound();

        return Ok(card.ToViewModel());
    }

    [HttpGet("numberCard/{numberCard:minlength(20): maxlength(20)}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CardViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByNumberCard(string numberCard)
    {
        var card = await _cardService.GetByNumberCardAsync(numberCard);

        if (card is null)
            return NotFound();

        return Ok(card.ToViewModel());
    }

    [HttpGet("userId/{userId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CardViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var cards = await _cardService.GetByUserIdAsync(userId);

        if (cards is null)
            return NotFound();

        return Ok(cards.ToViewModel());
    }
}
