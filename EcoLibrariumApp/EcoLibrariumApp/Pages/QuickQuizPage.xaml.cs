using EcoLibrariumApp.Services;
using EcoLibrariumApp.ViewModels;


namespace EcoLibrariumApp;

public partial class QuickQuizPage : ContentPage
{
    public QuickQuizPage()
    {
        //AuthenticationService.LogIn("andrej2431@gmail.com", "24312431").Wait();

        InitializeComponent();
        BindingContext = new QuickQuizViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await AuthenticationService.LogIn("andrej2431@gmail.com", "24312431");

        if (!AuthenticationService.IsAdmin())
        {
            await NavigationService.NavigateTo(new MainMenuPage());
        }
    }
}