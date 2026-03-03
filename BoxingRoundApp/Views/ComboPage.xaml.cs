using BoxingRoundApp.ViewModel;

namespace BoxingRoundApp.Views;

public partial class ComboPage : ContentPage
{
	public ComboPage(ComboViewModel comboViewModel)
	{
		InitializeComponent();
		BindingContext = comboViewModel;
	}
}