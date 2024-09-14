using Microsoft.AspNetCore.Mvc;
using Moq;
using Users.API.Application.Extensions;
using Users.API.Controllers;
using Users.API.Services;
using Users.API.Services.DTOs;

namespace Users.UnitTests.Controllers;

public class UserControllerTests
{
    [Fact]
    public async Task GetById_ShouldReturnOkWithUser_WhenUserExists()
    {
        int id = 123456;
        var user = new UserDTO(id, "User 1", "Document 1", "user1@easypay.com", DateOnly.MinValue, 0m);

        var userService = new Mock<IUserService>();

        userService.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(user)!);

        var userController = new UserController(userService.Object);

        var result = await userController.GetById(id);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(user.ToViewModel(), okResult.Value);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenUserDoesNotExists()
    {
        int id = 123456;
        UserDTO? user = null;

        var userService = new Mock<IUserService>();

        userService.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(user));

        var userController = new UserController(userService.Object);

        var result = await userController.GetById(id);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetByDocument_ShouldReturnOkWithUser_WhenUserExists()
    {
        string document = "123456789";
        var user = new UserDTO(123456, "User 1", document, "user1@easypay.com", DateOnly.MinValue, 0m);

        var userService = new Mock<IUserService>();

        userService.Setup(x => x.GetByDocumentAsync(document)).Returns(Task.FromResult(user)!);

        var userController = new UserController(userService.Object);

        var result = await userController.GetByDocument(document);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(user.ToViewModel(), okResult.Value);
    }

    [Fact]
    public async Task GetByDocument_ShouldReturnNotFound_WhenUserDoesNotExists()
    {
        string document = "123456789";
        UserDTO? user = null;

        var userService = new Mock<IUserService>();

        userService.Setup(x => x.GetByDocumentAsync(document)).Returns(Task.FromResult(user));

        var userController = new UserController(userService.Object);

        var result = await userController.GetByDocument(document);

        Assert.IsType<NotFoundResult>(result);
    }
}
