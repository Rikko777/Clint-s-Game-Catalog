using ClintsCatalog.Core.Models;
using ClintsCatalog.Data.Context;
using ClintsCatalog.Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ClintsCatalog.Tests;

public class GameTitleRepositoryTests : IDisposable
{
  private readonly AppDbContext _context;
  private readonly GameTitleRepository _repository;

  public GameTitleRepositoryTests()
  {
    var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

    _context = new AppDbContext(options);
    _repository = new GameTitleRepository(_context);
  }

  private static GameTitle MakeGame(string title = "Doom", string publisher = "id Software",
      string developer = "id Software", MediaType media = MediaType.CD,
      PackagingType packaging = PackagingType.BigBox) =>
      new() { Title = title, Publisher = publisher, Developer = developer, Media = media, Packaging = packaging };

  [Fact]
  public async Task AddAsync_ShouldAddGameToDatabase()
  {
    var game = MakeGame();

    await _repository.AddAsync(game);

    _context.GameTitles.Should().HaveCount(1);
    _context.GameTitles.First().Title.Should().Be("Doom");
  }

  [Fact]
  public async Task GetAllAsync_ShouldReturnAllGames()
  {
    await _repository.AddAsync(MakeGame("Doom"));
    await _repository.AddAsync(MakeGame("Quake"));

    var result = await _repository.GetAllAsync();

    result.Should().HaveCount(2);
  }

  [Fact]
  public async Task GetAllAsync_ShouldReturnGamesOrderedByTitle()
  {
    await _repository.AddAsync(MakeGame("Quake"));
    await _repository.AddAsync(MakeGame("Doom"));

    var result = await _repository.GetAllAsync();

    result.First().Title.Should().Be("Doom");
    result.Last().Title.Should().Be("Quake");
  }

  [Fact]
  public async Task GetByIdAsync_ShouldReturnCorrectGame()
  {
    var game = MakeGame("Half-Life");
    await _repository.AddAsync(game);

    var result = await _repository.GetByIdAsync(game.Id);

    result.Should().NotBeNull();
    result!.Title.Should().Be("Half-Life");
  }

  [Fact]
  public async Task GetByIdAsync_ShouldReturnNull_WhenGameDoesNotExist()
  {
    var result = await _repository.GetByIdAsync(999);

    result.Should().BeNull();
  }

  [Fact]
  public async Task UpdateAsync_ShouldUpdateGameInDatabase()
  {
    var game = MakeGame("Doom");
    await _repository.AddAsync(game);

    game.Title = "Doom II";
    await _repository.UpdateAsync(game);

    var updated = await _repository.GetByIdAsync(game.Id);
    updated!.Title.Should().Be("Doom II");
  }

  [Fact]
  public async Task DeleteAsync_ShouldRemoveGameFromDatabase()
  {
    var game = MakeGame("Doom");
    await _repository.AddAsync(game);

    await _repository.DeleteAsync(game.Id);

    _context.GameTitles.Should().BeEmpty();
  }

  [Fact]
  public async Task DeleteAsync_ShouldThrow_WhenGameDoesNotExist()
  {
    Func<Task> act = async () => await _repository.DeleteAsync(999);

    await act.Should().ThrowAsync<KeyNotFoundException>();
  }

  [Fact]
  public async Task SearchAsync_ShouldFilterByTitle()
  {
    await _repository.AddAsync(MakeGame("Doom"));
    await _repository.AddAsync(MakeGame("Quake"));

    var result = await _repository.SearchAsync("doom", null, null);

    result.Should().HaveCount(1);
    result.First().Title.Should().Be("Doom");
  }

  [Fact]
  public async Task SearchAsync_ShouldBeCaseInsensitive()
  {
    await _repository.AddAsync(MakeGame("Doom"));

    var result = await _repository.SearchAsync("DOOM", null, null);

    result.Should().HaveCount(1);
  }

  [Fact]
  public async Task SearchAsync_ShouldFilterByPublisher()
  {
    await _repository.AddAsync(MakeGame("Doom", publisher: "id Software"));
    await _repository.AddAsync(MakeGame("Warcraft", publisher: "Blizzard"));

    var result = await _repository.SearchAsync("Blizzard", null, null);

    result.Should().HaveCount(1);
    result.First().Title.Should().Be("Warcraft");
  }

  [Fact]
  public async Task SearchAsync_ShouldFilterByMediaType()
  {
    await _repository.AddAsync(MakeGame("Doom", media: MediaType.CD));
    await _repository.AddAsync(MakeGame("Prince of Persia", media: MediaType.Diskette));

    var result = await _repository.SearchAsync(null, MediaType.Diskette, null);

    result.Should().HaveCount(1);
    result.First().Title.Should().Be("Prince of Persia");
  }

  [Fact]
  public async Task SearchAsync_ShouldFilterByPackagingType()
  {
    await _repository.AddAsync(MakeGame("Doom", packaging: PackagingType.BigBox));
    await _repository.AddAsync(MakeGame("Myst", packaging: PackagingType.JewelCase));

    var result = await _repository.SearchAsync(null, null, PackagingType.JewelCase);

    result.Should().HaveCount(1);
    result.First().Title.Should().Be("Myst");
  }

  [Fact]
  public async Task SearchAsync_ShouldReturnAll_WhenNoFiltersApplied()
  {
    await _repository.AddAsync(MakeGame("Doom"));
    await _repository.AddAsync(MakeGame("Quake"));
    await _repository.AddAsync(MakeGame("StarCraft"));

    var result = await _repository.SearchAsync(null, null, null);

    result.Should().HaveCount(3);
  }

  public void Dispose()
  {
    _context.Dispose();
  }
}
