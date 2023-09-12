using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace EcoLibrariumApp.ViewModels
{
    public partial class MainMenuViewModel : ObservableObject 
    {
        [ObservableProperty]
        string searchValue;


        [RelayCommand]
        async Task Logout()
        {


            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage()); // TODO NAVIGATION
        }
    }
}
