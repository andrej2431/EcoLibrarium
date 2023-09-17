using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoLibrariumApp.Services;


namespace EcoLibrariumApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;

        [RelayCommand]
        async Task LogIn()
        {
            AuthenticationResult result = await AuthenticationService.LogIn(Email, Password);

            if (result.Success)
            {
                await NavigationService.NavigateTo(new MainMenuPage());
            }
            else
            {
                await MessageService.showMessage(result.Message);
            }
        }


        [RelayCommand]
        async Task NavigateToRegisterPage()
        {
            await NavigationService.NavigateTo(new RegisterPage());
        }
    }
}