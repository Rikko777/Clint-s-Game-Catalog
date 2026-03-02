using ClintsCatalog.Core.Interfaces;
using ClintsCatalog.Core.Models;
using ClintsCatalog.Web.Components.Dialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ClintsCatalog.Web.Components.Pages;

public partial class Home
{
  [Inject] private IGameTitleRepository Repo { get; set; } = default!;
  [Inject] private IDialogService DialogService { get; set; } = default!;
  [Inject] private ISnackbar Snackbar { get; set; } = default!;

  private IEnumerable<GameTitle> _games = [];
  private string? _searchQuery;
  private MediaType? _selectedMedia;
  private PackagingType? _selectedPackaging;

  protected override async Task OnInitializedAsync() =>
      await LoadGamesAsync();

  private async Task LoadGamesAsync() =>
      _games = await Repo.SearchAsync(_searchQuery, _selectedMedia, _selectedPackaging);

  private async Task ApplyFilter(string query)
  {
    _searchQuery = query;
    await LoadGamesAsync();
  }

  private async Task OnMediaChanged(MediaType? media)
  {
    _selectedMedia = media;
    await LoadGamesAsync();
  }

  private async Task OnPackagingChanged(PackagingType? packaging)
  {
    _selectedPackaging = packaging;
    await LoadGamesAsync();
  }

  private async Task OpenAddDialog()
  {
    var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };
    var dialog = await DialogService.ShowAsync<GameDialog>("Add Game", options);
    var result = await dialog.Result;
    if (result is { Canceled: false })
      await LoadGamesAsync();
  }

  private async Task OpenEditDialog(GameTitle game)
  {
    var parameters = new DialogParameters<GameDialog> { { x => x.Game, game } };
    var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };
    var dialog = await DialogService.ShowAsync<GameDialog>("Edit Game", parameters, options);
    var result = await dialog.Result;
    if (result is { Canceled: false })
      await LoadGamesAsync();
  }

  private async Task DeleteAsync(GameTitle game)
  {
    bool? confirmed = await DialogService.ShowMessageBox(
        "Confirm Delete",
        $"Are you sure you want to delete '{game.Title}'?",
        yesText: "Delete", cancelText: "Cancel");

    if (confirmed != true) return;

    await Repo.DeleteAsync(game.Id);
    Snackbar.Add($"'{game.Title}' deleted.", Severity.Success);
    await LoadGamesAsync();
  }
}
