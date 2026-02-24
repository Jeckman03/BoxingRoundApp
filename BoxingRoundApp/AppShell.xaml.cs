using BoxingRoundApp.Views;

namespace BoxingRoundApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(CreateWorkoutProfilePage), typeof(CreateWorkoutProfilePage));
            Routing.RegisterRoute(nameof(ActivateWorkoutProfilePage), typeof (ActivateWorkoutProfilePage));
        }
    }
}
