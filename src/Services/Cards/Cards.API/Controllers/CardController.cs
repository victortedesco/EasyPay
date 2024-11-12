using Cards.API.Application.Extensions;
using Cards.API.Application.ViewModels;
using Cards.API.Services;
using EasyPay.Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cards.API.Controllers;

[Route("api/cards")]
[Authorize]
[ApiController]
public class CardController(IKeyCloakService keyCloakService, ICardService cardService) : ControllerBase
{
    private readonly IKeyCloakService _keyCloakService = keyCloakService;
    private readonly ICardService _cardService = cardService;

    [HttpGet("id/{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CardViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = _keyCloakService.GetUserId();
        var userRoles = _keyCloakService.GetUserRoles();

        if (userId is null)
            return Unauthorized();

        var card = await _cardService.GetByIdAsync(id);

        if (card is null)
            return NotFound();

        if (!userRoles.Contains("admin") && card.UserId != userId)
            return Forbid();

        return Ok(card.ToViewModel());
    }

    [HttpGet("cardNumber/{cardNumber:minlength(20):maxlength(20)}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CardViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCardNumber(string cardNumber)
    {
        var userId = _keyCloakService.GetUserId();
        var userRoles = _keyCloakService.GetUserRoles();

        if (userId is null)
            return Unauthorized();

        var card = await _cardService.GetByCardNumberAsync(cardNumber);

        if (card is null)
            return NotFound();

        if (!userRoles.Contains("admin") && card.UserId != userId)
            return Forbid();

        return Ok(card.ToViewModel());
    }

    [HttpGet("userId/{userId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CardViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var keycloakId = _keyCloakService.GetUserId();
        var keycloakRoles = _keyCloakService.GetUserRoles();

        if (keycloakId is null)
            return Unauthorized();

        if (!keycloakRoles.Contains("admin") && keycloakId != userId)
            return Forbid();

        var cards = await _cardService.GetByUserIdAsync(userId);

        if (cards is null)
            return NotFound();

        return Ok(cards.ToViewModel());
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Add()
    {
        var userId = _keyCloakService.GetUserId();
        var userName = _keyCloakService.GetUserDisplayName();

        if (userId is null)
            return Unauthorized();

        var result = await _cardService.AddAsync(userId.Value, userName);

        if (result is null)
            return BadRequest();

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.ToViewModel());
    }
}
