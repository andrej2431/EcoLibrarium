using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoLibrariumApp.Services;

namespace EcoLibrariumApp.ViewModels
{
    public partial class MainMenuViewModel : ObservableObject 
    {
        [ObservableProperty]
        string searchValue;


        [RelayCommand]
        async Task NavigateToQuickSearch()
        {
            await NavigationService.NavigateTo(new SpeciesInfoPage());
        }

        [RelayCommand]
        async Task NavigateToQuickQuiz()
        {
            await NavigationService.NavigateTo(new QuickQuizPage());
        }

        [RelayCommand]
        async Task NavigateToSavedQuizzes()
        {
            await NavigationService.NavigateTo(new SavedQuizzesPage());
        }

        [RelayCommand]
        async Task NavigateToAdminMenu()
        {
            await NavigationService.NavigateTo(new AdminMenuPage());
        }

        [RelayCommand]
        async Task NavigateToSettings()
        {
            await NavigationService.NavigateTo(new SettingsPage());
        }

        [RelayCommand]
        async Task Logout()
        {
            AuthenticationResult result = await AuthenticationService.Logout();
            if(result.Success)
            {
                await NavigationService.NavigateTo(new LoginPage());
            } else
            {
                await MessageService.showMessage(result.Message);
            }
        }
    }
}
