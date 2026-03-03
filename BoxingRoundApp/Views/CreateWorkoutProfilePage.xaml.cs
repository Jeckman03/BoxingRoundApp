using BoxingRoundApp.Models;
using BoxingRoundApp.ViewModel;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Core;

namespace BoxingRoundApp.Views;

public partial class CreateWorkoutProfilePage : ContentPage
{
    private readonly CreateWorkoutProfileViewModel _viewModel;

	public CreateWorkoutProfilePage(CreateWorkoutProfileViewModel createWorkoutProfileViewModel)
	{
		InitializeComponent();
		BindingContext = createWorkoutProfileViewModel;
        _viewModel = createWorkoutProfileViewModel;
	}

    private void OnEntryFocused(object sender, FocusEventArgs e)
    {
        var entry = (Entry)sender;

        if (entry.Text == "0")
        {
            entry.Text = string.Empty;
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadProfileToEditAsync();
    }

    private async void OnEditComboClicked(object sender, EventArgs e)
    {
        var round = (RoundSettingsModel)((VisualElement)sender).BindingContext;
        var popup = new ComboPickerPopup(round.RoundDescription);

        var result = await this.ShowPopupAsync(popup);

        if (result == null) return;

        if (result != null)
        {
            string raw = result.ToString();
            string cleanCombo = "";

            if (raw.Contains("WasDismissedByTappingOutsideOfPopup = True"))
            {
                // User clicked away - EXIT the method without updating the model
                return;
            }

            if (raw.Contains("Result ="))
            {
                // 1. Get everything after "Result = "
                var afterResult = raw.Split(new[] { "Result =" }, StringSplitOptions.None)[1];

                // 2. Find where the metadata starts (usually " , WasDismissed" or just ", Was")
                // We split by the first occurrence of ", WasDismissed" to keep all your hits
                var comboPart = afterResult.Split(new[] { ", WasDismissed" }, StringSplitOptions.None)[0];

                cleanCombo = comboPart.Trim().TrimEnd('}');
            }
            else
            {
                cleanCombo = raw;
            }

            // Assign back to your Round model
            if (sender is Button btn && btn.BindingContext is RoundSettingsModel roundSelected)
            {
                roundSelected.RoundDescription = cleanCombo;
            }
        }
    }
}