using BoxingRoundApp.ViewModel;

namespace BoxingRoundApp
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
            _viewModel = mainPageViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadWorkoutProfilesAsync();
        }
    }
}
