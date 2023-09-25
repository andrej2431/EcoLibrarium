using EcoLibrariumApp.ViewModels;
using EcoLibrariumApp.Services;

namespace EcoLibrariumApp;

public partial class MainMenuPage : ContentPage
{
	public MainMenuPage()
	{

		InitializeComponent();
		BindingContext = new MainMenuViewModel();
		
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();


        if (!AuthenticationService.IsLoggedIn())
        {
            await NavigationService.NavigateTo(new LoginPage());
        }

		if(AuthenticationService.IsAdmin())
		{
            AdminBtn.IsVisible = true;
        } else
		{
			AdminBtn.IsVisible = false;
        }

		
    }
}