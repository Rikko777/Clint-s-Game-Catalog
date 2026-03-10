using ClintsCatalog.Core.Interfaces;
using ClintsCatalog.Core.Models;
using ClintsCatalog.Data.Context;
using ClintsCatalog.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
  Args = args,
  EnvironmentName = "Development"
});

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
        new GameTitle { Title = "Diablo", Publisher = "Blizzard Entertainment", Developer = "Blizzard Entertainment", Barcode = "012345678905", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Half-Life", Publisher = "Sierra Studios", Developer = "Valve", Barcode = "012345678906", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Age of Empires II", Publisher = "Microsoft", Developer = "Ensemble Studios", Barcode = "012345678907", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Baldur's Gate", Publisher = "Interplay", Developer = "BioWare", Barcode = "012345678908", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Tomb Raider", Publisher = "Eidos Interactive", Developer = "Core Design", Barcode = "012345678910", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Command & Conquer", Publisher = "Virgin Interactive", Developer = "Westwood Studios", Barcode = "012345678915", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Unreal Tournament", Publisher = "GT Interactive", Developer = "Epic Games", Barcode = "012345678920", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Thief: The Dark Project", Publisher = "Eidos Interactive", Developer = "Looking Glass Studios", Barcode = "012345678921", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Roller Coaster Tycoon", Publisher = "Hasbro Interactive", Developer = "Chris Sawyer", Barcode = "012345678922", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "The Sims", Publisher = "Electronic Arts", Developer = "Maxis", Barcode = "012345678923", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Red Alert 2", Publisher = "Electronic Arts", Developer = "Westwood Studios", Barcode = "012345678924", Media = MediaType.CD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "StarCraft", Publisher = "Blizzard Entertainment", Developer = "Blizzard Entertainment", Barcode = "012345678904", Media = MediaType.CD, Packaging = PackagingType.JewelCase },
        new GameTitle { Title = "Myst", Publisher = "Broderbund", Developer = "Cyan", Barcode = "012345678909", Media = MediaType.CD, Packaging = PackagingType.JewelCase },
        new GameTitle { Title = "Need for Speed II", Publisher = "Electronic Arts", Developer = "EA Canada", Barcode = "012345678911", Media = MediaType.CD, Packaging = PackagingType.JewelCase },
        new GameTitle { Title = "Quake III Arena", Publisher = "Activision", Developer = "id Software", Barcode = "012345678925", Media = MediaType.CD, Packaging = PackagingType.JewelCase },
        new GameTitle { Title = "Age of Empires", Publisher = "Microsoft", Developer = "Ensemble Studios", Barcode = "012345678926", Media = MediaType.CD, Packaging = PackagingType.JewelCase },
        new GameTitle { Title = "Diablo II", Publisher = "Blizzard Entertainment", Developer = "Blizzard Entertainment", Barcode = "012345678927", Media = MediaType.CD, Packaging = PackagingType.JewelCase },
        new GameTitle { Title = "Warcraft III", Publisher = "Blizzard Entertainment", Developer = "Blizzard Entertainment", Barcode = "012345678928", Media = MediaType.CD, Packaging = PackagingType.JewelCase },
        new GameTitle { Title = "Civilization II", Publisher = "MicroProse", Developer = "MicroProse", Barcode = "012345678930", Media = MediaType.CD, Packaging = PackagingType.SmallBox },
        new GameTitle { Title = "Transport Tycoon", Publisher = "MicroProse", Developer = "Chris Sawyer", Barcode = "012345678931", Media = MediaType.CD, Packaging = PackagingType.SmallBox },
        new GameTitle { Title = "Dungeon Keeper", Publisher = "Electronic Arts", Developer = "Bullfrog Productions", Barcode = "012345678932", Media = MediaType.CD, Packaging = PackagingType.SmallBox },
        new GameTitle { Title = "Microsoft Flight Simulator 98", Publisher = "Microsoft", Developer = "Microsoft", Barcode = "012345678940", Media = MediaType.CD, Packaging = PackagingType.Sleeve },
        new GameTitle { Title = "Encarta 97", Publisher = "Microsoft", Developer = "Microsoft", Barcode = "012345678941", Media = MediaType.CD, Packaging = PackagingType.Sleeve },
        new GameTitle { Title = "Doom II", Publisher = "id Software", Developer = "id Software", Barcode = "012345678950", Media = MediaType.CD, Packaging = PackagingType.None },
        new GameTitle { Title = "Heretic", Publisher = "id Software", Developer = "Raven Software", Barcode = "012345678951", Media = MediaType.CD, Packaging = PackagingType.None },
        new GameTitle { Title = "SimCity 2000", Publisher = "Maxis", Developer = "Maxis", Barcode = "012345678912", Media = MediaType.Diskette, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Prince of Persia", Publisher = "Broderbund", Developer = "Jordan Mechner", Barcode = "012345678913", Media = MediaType.Diskette, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Monkey Island 2", Publisher = "LucasArts", Developer = "LucasArts", Barcode = "012345678914", Media = MediaType.Diskette, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Wolfenstein 3D", Publisher = "Apogee Software", Developer = "id Software", Barcode = "012345678960", Media = MediaType.Diskette, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Wing Commander", Publisher = "Origin Systems", Developer = "Origin Systems", Barcode = "012345678961", Media = MediaType.Diskette, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Ultima VII", Publisher = "Origin Systems", Developer = "Origin Systems", Barcode = "012345678962", Media = MediaType.Diskette, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Commander Keen", Publisher = "Apogee Software", Developer = "id Software", Barcode = "012345678963", Media = MediaType.Diskette, Packaging = PackagingType.SmallBox },
        new GameTitle { Title = "Lemmings", Publisher = "Psygnosis", Developer = "DMA Design", Barcode = "012345678964", Media = MediaType.Diskette, Packaging = PackagingType.SmallBox },
        new GameTitle { Title = "MS-DOS 6.22", Publisher = "Microsoft", Developer = "Microsoft", Barcode = "012345678965", Media = MediaType.Diskette, Packaging = PackagingType.Sleeve },
        new GameTitle { Title = "Windows 3.1", Publisher = "Microsoft", Developer = "Microsoft", Barcode = "012345678966", Media = MediaType.Diskette, Packaging = PackagingType.Sleeve },
        new GameTitle { Title = "Oblivion", Publisher = "2K Games", Developer = "Bethesda", Barcode = "012345678970", Media = MediaType.DVD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Spore", Publisher = "Electronic Arts", Developer = "Maxis", Barcode = "012345678971", Media = MediaType.DVD, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Half-Life 2", Publisher = "Valve", Developer = "Valve", Barcode = "012345678972", Media = MediaType.DVD, Packaging = PackagingType.JewelCase },
        new GameTitle { Title = "Sims 2", Publisher = "Electronic Arts", Developer = "Maxis", Barcode = "012345678973", Media = MediaType.DVD, Packaging = PackagingType.JewelCase },
        new GameTitle { Title = "Zork I", Publisher = "Infocom", Developer = "Infocom", Barcode = "012345678980", Media = MediaType.Tape, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Hitchhiker's Guide to the Galaxy", Publisher = "Infocom", Developer = "Infocom", Barcode = "012345678981", Media = MediaType.Tape, Packaging = PackagingType.BigBox },
        new GameTitle { Title = "Cyberpunk 2077", Publisher = "CD Projekt", Developer = "CD Projekt Red", Barcode = "012345678990", Media = MediaType.Bluray, Packaging = PackagingType.SmallBox },
        new GameTitle { Title = "Baldur's Gate 3", Publisher = "Larian Studios", Developer = "Larian Studios", Barcode = "012345678991", Media = MediaType.Bluray, Packaging = PackagingType.SmallBox }
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
