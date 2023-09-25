using EcoLibrariumApp.Services;
using EcoLibrariumApp.ViewModels;

namespace EcoLibrariumApp;

public partial class AdminMenuPage : ContentPage
{
	public AdminMenuPage()
	{
		InitializeComponent();

        BindingContext = new AdminMenuViewModel();

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();


        if (!AuthenticationService.IsAdmin())
        {
            await NavigationService.NavigateTo(new MainMenuPage());
        }
    }
}