using EasyPay.Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transactions.API.Application.Extensions;
using Transactions.API.Application.Requests;
using Transactions.API.Application.ViewModels;
using Transactions.Infrastructure.Abstractions;
using Transactions.Infrastructure.DTOs;

namespace Transactions.API.Controllers;

[Route("api/transactions")]
[Authorize]
[ApiController]
public class TransactionsController(IKeyCloakService keyCloakService, ITransactionService transactionService) : ControllerBase
{
    private readonly IKeyCloakService _keyCloakService = keyCloakService;
    private readonly ITransactionService _transactionService = transactionService;

    [HttpGet("id/{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(TransactionViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = _keyCloakService.GetUserId();
        var userRoles = _keyCloakService.GetUserRoles();

        if (Guid.Empty == userId)
            return Unauthorized();

        var transaction = await _transactionService.GetByIdAsync(id);

        if (transaction is null)
            return NotFound();

        if (!userRoles.Contains("admin") && transaction.SenderId != userId && transaction.RecipientId != userId)
            return Forbid();

        return Ok(transaction.ToViewModel());
    }

    [HttpGet("senderId/{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(TransactionViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySenderId(Guid id)
    {
        var userId = _keyCloakService.GetUserId();
        var userRoles = _keyCloakService.GetUserRoles();

        if (Guid.Empty == userId)
            return Unauthorized();

        if (!userRoles.Contains("admin") && id != userId)
            return Forbid();

        var transactions = await _transactionService.GetBySenderIdAsync(id);

        if (!transactions.Any())
            return NoContent();

        return Ok(transactions.ToViewModel());
    }

    [HttpGet("recipientId/{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(IEnumerable<TransactionViewModel>), StatusCodes.Status200OK)]

    public async Task<IActionResult> GetByIdRecipient(Guid id)
    {
        var userId = _keyCloakService.GetUserId();
        var userRoles = _keyCloakService.GetUserRoles();

        if (Guid.Empty == userId)
            return Unauthorized();

        if (!userRoles.Contains("admin") && id != userId)
            return Forbid();

        var transactions = await _transactionService.GetByRecipientIdAsync(id);

        if (!transactions.Any())
            return NoContent();

        return Ok(transactions.ToViewModel());
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(TransactionViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add([FromBody] AddTransactionRequest request)
    {
        var userId = _keyCloakService.GetUserId();
        var userRoles = _keyCloakService.GetUserRoles();

        if (Guid.Empty == userId)
            return Unauthorized();

        if (request.RecipientId == userId)
            return Forbid();

        var dto = new TransactionDTO(Guid.Empty, userId, request.RecipientId, request.Amount, DateTime.UtcNow);

        var transaction = await _transactionService.AddAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = transaction.Id}, transaction.ToViewModel());
    }
}

