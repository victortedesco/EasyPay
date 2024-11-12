using Moq;

namespace Cards.UnitTests.Controllers;

public class CardControllerTests
{
    [Fact]
    public async Task GetById_ShouldReturnCard_WhenCardExists()
    {
        int id = 123456;

        var card = new CardDTO(id, "20202020202020202020", "123", DateOnly.FromDateTime(DateTime.Now.AddYears(3)), "12345", 100M, 500.50M);

        var cardService = new Mock<ICardService>();

        cardService.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(card));

        var cardController = new CardController(cardService.Object);

        var result = await cardController.GetById(id);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(card.ToViewModel(), okResult.Value);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenCardDoesNotExists()
    {
        int id = 123456;
        CardDTO? card = null;

        var cardService = new Mock<ICardService>();

        cardService.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(card));

        var cardController = new CardController(cardService.Object);

        var result = await cardController.GetById(id);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetByNumberCard_ShouldReturnOkWithCard_WhenCardExists()
    {
        string numberCard = "20202020202020202020";
        var card = new CardDTO(123456, "20202020202020202020", "123", DateOnly.FromDateTime(DateTime.Now.AddYears(3)), "12345", 100M, 500.50M);

        var cardService = new Mock<ICardService>();

        cardService.Setup(x => x.GetByNumberCardAsync(numberCard)).Returns(Task.FromResult(card)!);

        var cardController = new CardController(cardService.Object);

        var result = await cardController.GetByNumberCard(numberCard);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(card.ToViewModel(), okResult.Value);
    }

    [Fact]
    public async Task GetByNumberCard_ShouldReturnNotFound_WhenCardDoesNotExists()
    {
        string numberCard = "20202020202020202020";
        CardDTO? card = null;

        var cardService = new Mock<ICardService>();

        cardService.Setup(x => x.GetByNumberCardAsync(numberCard)).Returns(Task.FromResult(card));

        var cardController = new CardController(cardService.Object);

        var result = await cardController.GetByNumberCard(numberCard);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetByUserId_ShouldReturnOkWithCard_WhenCardExists()
    {
        string userId = "123";
        var cards = new List<CardDTO?>
        {
            new CardDTO(123456, "20202020202020202020", "123", DateOnly.FromDateTime(DateTime.Now.AddYears(3)), "12345", 100M, 500.50M),
            new CardDTO(123456, "20202020202020202020", "123", DateOnly.FromDateTime(DateTime.Now.AddYears(3)), "12345", 100M, 500.50M)
        };

        var cardService = new Mock<ICardService>();

        cardService.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync(cards);

        var cardController = new CardController(cardService.Object);

        var result = await cardController.GetByUserId(userId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(cards.ToViewModel(), okResult.Value);
    }


    [Fact]
    public async Task GetByUserId_ShouldReturnNotFound_WhenCardExists()
    {
        string userId = "123";
        IEnumerable<CardDTO?> cards = null;

        var cardService = new Mock<ICardService>();

        cardService.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync(cards);

        var cardController = new CardController(cardService.Object);

        var result = await cardController.GetByUserId(userId);

        Assert.IsType<NotFoundResult>(result);
    }
}
