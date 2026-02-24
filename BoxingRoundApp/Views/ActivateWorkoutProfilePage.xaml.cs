using BoxingRoundApp.ViewModel;

namespace BoxingRoundApp.Views;

public partial class ActivateWorkoutProfilePage : ContentPage
{
	public ActivateWorkoutProfilePage(ActivateWorkoutProfileViewModel activateWorkoutProfileViewModel)
	{
		InitializeComponent();
		BindingContext = activateWorkoutProfileViewModel;
	}
}