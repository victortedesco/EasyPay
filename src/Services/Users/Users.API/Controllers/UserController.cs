using EasyPay.Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.API.Application.Extensions;
using Users.API.Application.Requests;
using Users.API.Application.ViewModels;
using Users.API.Services;
using Users.API.Services.DTOs;

namespace Users.API.Controllers;

[Route("api/users")]
[Authorize]
[ApiController]
public class UserController(ILogger<UserController> logger, IKeyCloakService keyCloakService, IUserService userService) : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;
    private readonly IKeyCloakService _keyCloakService = keyCloakService;
    private readonly IUserService _userService = userService;

    [HttpGet("id/{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var keycloakId = _keyCloakService.GetUserId();
        var keycloakRoles = _keyCloakService.GetUserRoles();

        if (keycloakId == Guid.Empty)
            return Unauthorized();

        if (keycloakId != id && !keycloakRoles.Contains("admin"))
            return Forbid();

        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        return Ok(user.ToViewModel());
    }

    [HttpGet("document/{document:minlength(11):maxlength(14)}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDocument(string document)
    {
        var user = await _userService.GetByDocumentAsync(document);

        if (user is null)
            return NotFound();

        return Ok(user.ToViewModel());
    }

    [HttpGet("email/{email:minlength(5):maxlength(100)}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var keycloakId = _keyCloakService.GetUserId();
        var keycloakRoles = _keyCloakService.GetUserRoles();

        if (keycloakId == Guid.Empty)
            return Unauthorized();

        var user = await _userService.GetByEmailAsync(email);
        if (user is null)
            return NotFound();

        if (keycloakId != user.Id && !keycloakRoles.Contains("admin"))
            return Forbid();

        return Ok(user.ToViewModel());
    }

    [HttpGet("balance/{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBalance(Guid id)
    {
        var keycloakId = _keyCloakService.GetUserId();
        var keycloakRoles = _keyCloakService.GetUserRoles();

        if (keycloakId == Guid.Empty)
            return Unauthorized();

        if (keycloakId != id && !keycloakRoles.Contains("admin"))
            return Forbid();

        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        return Ok(user.Balance);
    }

    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add([FromBody] AddUserRequest request)
    {
        var keycloakId = _keyCloakService.GetUserId();
        var keycloakRoles = _keyCloakService.GetUserRoles();

        if (keycloakId == Guid.Empty)
            return Unauthorized();

        if (request.Name == "admin")
            return BadRequest();

        var user = new UserDTO(keycloakId, request.Name, request.Document, request.Email, 0);
        user = await _userService.AddAsync(user);

        if (user is null)
            return BadRequest();

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user.ToViewModel());
    }

    [HttpPatch("balance/{id:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SetBalance(Guid id, [FromBody] decimal balance)
    {
        var keycloakId = _keyCloakService.GetUserId();
        var keycloakRoles = _keyCloakService.GetUserRoles();

        if (keycloakId == Guid.Empty)
            return Unauthorized();

        if (keycloakId != id)
            return Forbid();

        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        if (balance < -200)
            return BadRequest();

        await _userService.SetUserBalance(id, balance);

        return Ok();
    }
}
