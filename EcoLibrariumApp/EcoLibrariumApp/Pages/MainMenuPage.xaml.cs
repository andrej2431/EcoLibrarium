using EcoLibrariumApp.ViewModels;

namespace EcoLibrariumApp;

public partial class MainMenuPage : ContentPage
{
	public MainMenuPage(string sessionId)
	{
		InitializeComponent();
		BindingContext = new MainMenuViewModel();
	}
}