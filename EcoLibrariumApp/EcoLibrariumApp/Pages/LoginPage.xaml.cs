using EcoLibrariumApp.ViewModels;

namespace EcoLibrariumApp;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel(); 
	}
}