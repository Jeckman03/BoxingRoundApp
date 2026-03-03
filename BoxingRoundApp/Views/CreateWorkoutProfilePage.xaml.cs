using BoxingRoundApp.Models;
using BoxingRoundApp.ViewModel;
using CommunityToolkit.Maui.Extensions;

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
        var popup = new ComboPickerPopup();

        var result = await this.ShowPopupAsync(popup);

        // result is an object, so we convert it to string
        if (result != null && !string.IsNullOrWhiteSpace(result.ToString()))
        {
            string finalCombo = result.ToString();

            var round = (RoundSettingsModel)((VisualElement)sender).BindingContext;

            // USE THE PROPERTY (Capital R), NOT THE FIELD (lowercase r)
            // This triggers the "Hey UI, I changed!" event.
            round.RoundDescription = finalCombo;
        }
    }
}