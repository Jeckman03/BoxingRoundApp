using BoxingRoundApp.ViewModel;

namespace BoxingRoundApp.Views;

public partial class CreateWorkoutProfilePage : ContentPage
{
	public CreateWorkoutProfilePage(CreateWorkoutProfileViewModel createWorkoutProfileViewModel)
	{
		InitializeComponent();
		BindingContext = createWorkoutProfileViewModel;
	}
}