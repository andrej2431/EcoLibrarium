using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoLibrariumApp.Services;

namespace EcoLibrariumApp.ViewModels
{

    public partial class AdminMenuViewModel : ObservableObject
    {

        [RelayCommand]
        async Task NavigateToAddSpecies()
        {
            await NavigationService.NavigateTo(new AddSpeciesPage());
        }

        [RelayCommand]
        async Task NavigateToMainMenu()
        {
            await NavigationService.NavigateTo(new MainMenuPage());
        }

        [RelayCommand]
        async Task NavigateToUploadQuiz()
        {
            await NavigationService.NavigateTo(new UploadQuizPage());
        }

        [RelayCommand]
        async Task PromoteUserToAdmin()
        {
            await NavigationService.NavigateTo(new PromoteUserPage());
        }
    }
}
