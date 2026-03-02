using ClintsCatalog.Core.Interfaces;
using ClintsCatalog.Core.Models;
using ClintsCatalog.Data.Context;
using ClintsCatalog.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddScoped<IGameTitleRepository, GameTitleRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
  await db.Database.MigrateAsync();

  if (!db.GameTitles.Any())
  {
    db.GameTitles.AddRange(
        new GameTitle { Title = "Doom", Publisher = "id Software", Developer = "id Software", Barcode = "012345678901", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Quake", Publisher = "id Software", Developer = "id Software", Barcode = "012345678902", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Warcraft II", Publisher = "Blizzard Entertainment", Developer = "Blizzard Entertainment", Barcode = "012345678903", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "StarCraft", Publisher = "Blizzard Entertainment", Developer = "Blizzard Entertainment", Barcode = "012345678904", Media = MediaType.CD, Packaging = PackagingType.JewelCase },
        new GameTitle { Title = "Diablo", Publisher = "Blizzard Entertainment", Developer = "Blizzard Entertainment", Barcode = "012345678905", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Half-Life", Publisher = "Sierra Studios", Developer = "Valve", Barcode = "012345678906", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Age of Empires II", Publisher = "Microsoft", Developer = "Ensemble Studios", Barcode = "012345678907", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Baldur's Gate", Publisher = "Interplay", Developer = "BioWare", Barcode = "012345678908", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Myst", Publisher = "Broderbund", Developer = "Cyan", Barcode = "012345678909", Media = MediaType.CD, Packaging = PackagingType.JewelCase },
        new GameTitle { Title = "Tomb Raider", Publisher = "Eidos Interactive", Developer = "Core Design", Barcode = "012345678910", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Need for Speed II", Publisher = "Electronic Arts", Developer = "EA Canada", Barcode = "012345678911", Media = MediaType.CD, Packaging = PackagingType.JewelCase },
        new GameTitle { Title = "SimCity 2000", Publisher = "Maxis", Developer = "Maxis", Barcode = "012345678912", Media = MediaType.Diskette, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Prince of Persia", Publisher = "Broderbund", Developer = "Jordan Mechner", Barcode = "012345678913", Media = MediaType.Diskette, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Monkey Island 2", Publisher = "LucasArts", Developer = "LucasArts", Barcode = "012345678914", Media = MediaType.Diskette, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Command & Conquer", Publisher = "Virgin Interactive", Developer = "Westwood Studios", Barcode = "012345678915", Media = MediaType.CD, Packaging = PackagingType.BigBox }
    );
    await db.SaveChangesAsync();
  }
}

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<ClintsCatalog.Web.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
