using BoxingRoundApp.ViewModel;

namespace BoxingRoundApp.Views;

public partial class CreateWorkoutProfilePage : ContentPage
{
	public CreateWorkoutProfilePage(CreateWorkoutProfileViewModel createWorkoutProfileViewModel)
	{
		InitializeComponent();
		BindingContext = createWorkoutProfileViewModel;
	}

    private void OnEntryFocused(object sender, FocusEventArgs e)
    {
        var entry = (Entry)sender;

        if (entry.Text == "0")
        {
            entry.Text = string.Empty;
        }
    }
}