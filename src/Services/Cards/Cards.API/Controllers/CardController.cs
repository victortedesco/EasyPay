using Cards.API.Application.Extensions;
using Cards.API.Application.ViewModels;
using Cards.API.Services;
using Cards.API.Services.DTOs;
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

    [HttpGet("{id:int}")]
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

    [HttpPut("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int id, [FromBody] CardViewModel request)
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

        var dto = new CardDTO(id, card.UserId, card.UserName, "", "", default, request.CardLimit, request.TotalExpenses);
        var result = await _cardService.UpdateAsync(id, dto);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(int id)
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

        var result = await _cardService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok();
    }
}
