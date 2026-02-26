
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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        

        DeviceDisplay.Current.KeepScreenOn = true;

        if (BindingContext is StartWorkoutViewModel viewModel)
        {
            viewModel.AudioManager.Initialize(HiddenPlayer);

            await viewModel.StartWorkoutAsync();
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        DeviceDisplay.Current.KeepScreenOn = false;
        viewModel._timerServices.Stop();
    }
}