using BoxingRoundApp.ViewModel;

namespace BoxingRoundApp
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }
    }
}
