using ClintsCatalog.Core.Interfaces;
using ClintsCatalog.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ClintsCatalog.Web.Components.Dialogs;

public partial class GameDialog
{
  [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
  [Inject] private IGameTitleRepository Repo { get; set; } = default!;
  [Inject] private ISnackbar Snackbar { get; set; } = default!;

  [Parameter] public GameTitle? Game { get; set; }

  private string Title => Game is null ? "Add Game" : "Edit Game";

  private GameTitle _model = new()
  {
    Title = string.Empty,
    Publisher = string.Empty,
    Developer = string.Empty
  };

  protected override void OnParametersSet()
  {
    if (Game is not null)
    {
      _model = new GameTitle
      {
        Id = Game.Id,
        Title = Game.Title,
        Publisher = Game.Publisher,
        Developer = Game.Developer,
        Barcode = Game.Barcode,
        Media = Game.Media,
        Packaging = Game.Packaging
      };
    }
  }

  private async Task Submit()
  {
    if (string.IsNullOrWhiteSpace(_model.Title) ||
        string.IsNullOrWhiteSpace(_model.Publisher) ||
        string.IsNullOrWhiteSpace(_model.Developer))
    {
      Snackbar.Add("Please fill in all required fields.", Severity.Warning);
      return;
    }

    if (Game is null)
    {
      await Repo.AddAsync(_model);
      Snackbar.Add($"'{_model.Title}' added successfully.", Severity.Success);
    }
    else
    {
      await Repo.UpdateAsync(_model);
      Snackbar.Add($"'{_model.Title}' updated successfully.", Severity.Success);
    }

    MudDialog.Close(DialogResult.Ok(true));
  }

  private void Cancel() => MudDialog.Cancel();
}
