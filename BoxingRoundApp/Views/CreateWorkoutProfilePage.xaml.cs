using BoxingRoundApp.ViewModel;

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
}