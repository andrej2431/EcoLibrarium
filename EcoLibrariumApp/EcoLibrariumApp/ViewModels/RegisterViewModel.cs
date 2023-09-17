using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoLibrariumApp.Services;


namespace EcoLibrariumApp.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;


        [RelayCommand]
        async Task Register()
        {
            var registerResult = await AuthenticationService.Register(Username, Email, Password);

            if (registerResult.Success)
            {

                var loginResult = await AuthenticationService.LogIn(Email, Password);
                if (loginResult.Success)
                {
                    await NavigationService.NavigateTo(new MainMenuPage());
                }
                else
                {
                    await MessageService.showMessage($"Register successful, error during login: {loginResult.Message}");
                    await NavigationService.NavigateTo(new LoginPage());
                }
            }
            else
            {
                await MessageService.showMessage($"Error during registration: {registerResult.Message}");
            }
        }

        [RelayCommand]
        async Task NavigateToLoginPage()
        {
            await NavigationService.NavigateTo(new LoginPage());
        }
    }
}
