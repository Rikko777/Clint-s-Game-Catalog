using ClintsCatalog.Core.Models;

namespace ClintsCatalog.Core.Interfaces;

public interface IGameTitleRepository
{
  Task<IEnumerable<GameTitle>> GetAllAsync(CancellationToken ct = default);
  Task<GameTitle?> GetByIdAsync(int id, CancellationToken ct = default);
  Task AddAsync(GameTitle game, CancellationToken ct = default);
  Task UpdateAsync(GameTitle game, CancellationToken ct = default);
  Task DeleteAsync(int id, CancellationToken ct = default);
  Task<IEnumerable<GameTitle>> SearchAsync(string? query, MediaType? media, PackagingType? packaging, CancellationToken ct = default);
}
