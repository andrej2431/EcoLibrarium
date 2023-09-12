using EcoLibrariumApp.ViewModels;

namespace EcoLibrariumApp;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = new RegisterViewModel();
	}
}