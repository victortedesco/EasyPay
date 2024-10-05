using Cards.API.Infrastructure.Data;
using Cards.API.Services.Models;
using Cards.API.Services;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.UnitTests.Repositories;

public class CardRepositoryTests
{
    [Fact]
    public async Task GetById_ShouldReturnCard_WhenCardExists()
    {
        int id = 123456;

        var cards = new List<Card>
        {
            new() { Id = id, NumberCard = "20202020202020202020", SegureNumber = "123", UserId = "12345", TotalExpenses = 500.50M }
        };

        var mockContext = new Mock<ApplicationDataContext>();
        var cardsMock = cards.AsQueryable().BuildMockDbSet();

        cardsMock.Setup(x => x.FindAsync(id)).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return cards.FirstOrDefault(x => x.Id == id);
        });

        mockContext.Setup(a => a.Cards).Returns(cardsMock.Object);

        var cardService = new CardService(new CardRepository(mockContext.Object));

        var result = await cardService.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.Equivalent(cards[0], result);

    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenCardDoesNotExists()
    {
        int id = 123456;

        var cards = new List<Card>();

        var mockContext = new Mock<ApplicationDataContext>();
        var cardsMock = cards.AsQueryable().BuildMockDbSet();

        cardsMock.Setup(x => x.FindAsync(id)).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return cards.FirstOrDefault(x => x.Id == id);
        });

        mockContext.Setup(a => a.Cards).Returns(cardsMock.Object);

        var cardService = new CardService(new CardRepository(mockContext.Object));

        var result = await cardService.GetByIdAsync(id);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByNumberCard_ShouldReturnUser_WhenCardExists()
    {
        string numberCard = "20202020202020202020";

        var cards = new List<Card>
        {
            new() { Id = 123456, NumberCard = "20202020202020202020", SegureNumber = "123", UserId = "12345", TotalExpenses = 500.50M }
        };

        var mockContext = new Mock<ApplicationDataContext>();
        var cardsMock = cards.AsQueryable().BuildMockDbSet();

        mockContext.Setup(a => a.Cards).Returns(cardsMock.Object);

        var cardService = new CardService(new CardRepository(mockContext.Object));

        var result = await cardService.GetByNumberCardAsync(numberCard);

        Assert.NotNull(result);
        Assert.Equivalent(cards[0], result);
    }

    [Fact]
    public async Task GetByDocument_ShouldReturnNull_WhenUserDoesNotExists()
    {
        string numberCard = "123456";

        var cards = new List<Card>();

        var mockContext = new Mock<ApplicationDataContext>();
        var cardsMock = cards.AsQueryable().BuildMockDbSet();

        mockContext.Setup(a => a.Cards).Returns(cardsMock.Object);

        var userService = new CardService(new CardRepository(mockContext.Object));

        var result = await userService.GetByNumberCardAsync(numberCard);

        Assert.Null(result);

    }

    [Fact]
    public async Task GetbyUserId_ShouldReturnUserId_WhenUserIdExists()
    {
        string userId = "12345";
        var cards = new List<Card>
        {
            new() { Id = 123456, NumberCard = "20202020202020202020", SegureNumber = "123", UserId = "12345", TotalExpenses = 500.50M },
            new() { Id = 123454, NumberCard = "20202020202020202024", SegureNumber = "124", UserId = "12345", TotalExpenses = 500.50M }
        };

        var mockContext = new Mock<ApplicationDataContext>();
        var cardsMock = cards.AsQueryable().BuildMockDbSet();

        mockContext.Setup(a => a.Cards).Returns(cardsMock.Object);

        var cardsService = new CardService(new CardRepository(mockContext.Object));

        var result = await cardsService.GetByUserIdAsync(userId);

        Assert.Equal(cards.Count, result.Count());
        Assert.Equal(cards[0].Id, result.First().Id);
        Assert.Equal(cards[0].NumberCard, result.First().NumberCard);
        Assert.Equal(cards[0].SegureNumber, result.First().SegureNumber);
        Assert.Equal(cards[0].UserId, result.First().UserId);
        Assert.Equal(cards[0].TotalExpenses, result.First().TotalExpenses);
    }

}
