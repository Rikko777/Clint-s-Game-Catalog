using ClintsCatalog.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ClintsCatalog.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<GameTitle> GameTitles => Set<GameTitle>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<GameTitle>(entity =>
    {
      entity.HasKey(g => g.Id);

      entity.Property(g => g.Title)
          .IsRequired()
          .HasMaxLength(200);

      entity.Property(g => g.Publisher)
          .IsRequired()
          .HasMaxLength(200);

      entity.Property(g => g.Developer)
          .IsRequired()
          .HasMaxLength(200);

      entity.Property(g => g.Barcode)
          .HasMaxLength(50);
    });
  }
}
