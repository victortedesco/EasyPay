using MockQueryable.Moq;
using Moq;
using Users.API.Infrastructure.Data;
using Users.API.Services;
using Users.API.Services.Models;

namespace Users.UnitTests.Repositories;

public class UserRepositoryTests
{
    [Fact]
    public async Task GetById_ShouldReturnUser_WhenUserExists()
    {
        int id = 123456;

        var users = new List<User>
        {
            new() { Id = id, Name = "User 1", Document = "123456789", Email = "user1@easypay.com", BirthDate = DateOnly.MinValue, CreatedAt = DateTime.UtcNow }
        };

        var mockContext = new Mock<ApplicationDbContext>();
        var usersMock = users.AsQueryable().BuildMockDbSet();

        usersMock.Setup(x => x.FindAsync(id)).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return users.FirstOrDefault(x => x.Id == id);
        });

        mockContext.Setup(a => a.Users).Returns(usersMock.Object);

        var userRepository = new UserRepository(mockContext.Object);

        var result = await userRepository.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.Equivalent(users[0], result);
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenUserDoesNotExists()
    {
        int id = 123456;

        var users = new List<User>();

        var mockContext = new Mock<ApplicationDbContext>();
        var usersMock = users.AsQueryable().BuildMockDbSet();

        usersMock.Setup(x => x.FindAsync(id)).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return users.FirstOrDefault(x => x.Id == id);
        });

        mockContext.Setup(a => a.Users).Returns(usersMock.Object);

        var userRepository = new UserRepository(mockContext.Object);

        var result = await userRepository.GetByIdAsync(id);

        Assert.Null(result);
    }


    [Fact]
    public async Task GetByDocument_ShouldReturnUser_WhenUserExists()
    {
        string document = "123456";

        var users = new List<User>
        {
            new() { Id = 123456, Name = "User 1", Document = document, Email = "user1@easypay.com", BirthDate = DateOnly.MinValue, CreatedAt = DateTime.UtcNow }
        };

        var mockContext = new Mock<ApplicationDbContext>();
        var usersMock = users.AsQueryable().BuildMockDbSet();

        mockContext.Setup(a => a.Users).Returns(usersMock.Object);

        var userRepository = new UserRepository(mockContext.Object);

        var result = await userRepository.GetByDocumentAsync(document);

        Assert.NotNull(result);
        Assert.Equivalent(users[0], result);
    }

    [Fact]
    public async Task GetByDocument_ShouldReturnNull_WhenUserDoesNotExists()
    {
        string document = "123456";

        var users = new List<User>();

        var mockContext = new Mock<ApplicationDbContext>();
        var usersMock = users.AsQueryable().BuildMockDbSet();

        mockContext.Setup(a => a.Users).Returns(usersMock.Object);

        var userRepository = new UserRepository(mockContext.Object);

        var result = await userRepository.GetByDocumentAsync(document);

        Assert.Null(result);
    }
}
