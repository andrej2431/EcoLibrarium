namespace EcoLibrariumApp.Services
{

    internal class NavigationService
    {

        public NavigationService() { }

        public static async Task NavigateTo(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}