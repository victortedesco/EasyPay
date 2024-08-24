using Microsoft.AspNetCore.Mvc;
using Users.API.Application.Extensions;
using Users.API.Application.ViewModels;
using Users.API.Services;

namespace Users.API.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("id/{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        return Ok(user.ToViewModel());
    }

    [HttpGet("document/{document:minlength(11):maxlength(14)}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDocument(string document)
    {
        var user = await _userService.GetByDocumentAsync(document);

        if (user is null)
            return NotFound();

        return Ok(user.ToViewModel());
    }
}
