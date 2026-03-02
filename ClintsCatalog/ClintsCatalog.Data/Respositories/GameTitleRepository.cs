using ClintsCatalog.Core.Interfaces;
using ClintsCatalog.Core.Models;
using ClintsCatalog.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClintsCatalog.Data.Repositories;

public class GameTitleRepository(AppDbContext context) : IGameTitleRepository
{
  public async Task<IEnumerable<GameTitle>> GetAllAsync(CancellationToken ct = default) =>
      await context.GameTitles.AsNoTracking().OrderBy(g => g.Title).ToListAsync(ct);

  public async Task<GameTitle?> GetByIdAsync(int id, CancellationToken ct = default) =>
      await context.GameTitles.FindAsync([id], ct);

  public async Task AddAsync(GameTitle game, CancellationToken ct = default)
  {
    await context.GameTitles.AddAsync(game, ct);
    await context.SaveChangesAsync(ct);
  }

  public async Task UpdateAsync(GameTitle game, CancellationToken ct = default)
  {
    context.GameTitles.Update(game);
    await context.SaveChangesAsync(ct);
  }

  public async Task DeleteAsync(int id, CancellationToken ct = default)
  {
    var game = await GetByIdAsync(id, ct)
        ?? throw new KeyNotFoundException($"Game with id {id} was not found.");
    context.GameTitles.Remove(game);
    await context.SaveChangesAsync(ct);
  }

  public async Task<IEnumerable<GameTitle>> SearchAsync(string? query, MediaType? media,
      PackagingType? packaging, CancellationToken ct = default)
  {
    var q = context.GameTitles.AsNoTracking().AsQueryable();

    if (!string.IsNullOrWhiteSpace(query))
      q = q.Where(g => g.Title.Contains(query) ||
                       g.Publisher.Contains(query) ||
                       g.Developer.Contains(query));

    if (media.HasValue)
      q = q.Where(g => g.Media == media);

    if (packaging.HasValue)
      q = q.Where(g => g.Packaging == packaging);

    return await q.OrderBy(g => g.Title).ToListAsync(ct);
  }
}
