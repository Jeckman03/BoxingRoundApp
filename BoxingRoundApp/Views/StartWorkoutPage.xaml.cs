using BoxingRoundApp.ViewModel;

namespace BoxingRoundApp.Views;

public partial class StartWorkoutPage : ContentPage
{
    private StartWorkoutViewModel viewModel;

	public StartWorkoutPage(StartWorkoutViewModel startWorkoutViewModel)
	{
		InitializeComponent();
		BindingContext = startWorkoutViewModel;
        viewModel = startWorkoutViewModel;
	}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        viewModel._timerServices.Stop();
    }
}