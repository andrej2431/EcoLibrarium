using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoLibrariumApp.Services;
using System.Text;
using System.Text.Json;

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
            if (Username == null || Email == null || Password == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error 1", "Username, Email and Password cannot be empty.", "OK");
                return;
            }

            string passwordHash = EncryptionService.EncryptUsingSha256(Password);

            using (HttpClient httpClient = new HttpClient())
            {
                // REGISTER
                string jsonData = $"{{\"name\":\"{Username}\",\"email\":\"{Email}\",\"passwordHash\":\"{passwordHash}\"}}";
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7033/register", content); // TODO BETTER LINKS

                if (!response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Error 2", responseContent, "OK");
                    return;
                }

                // LOGIN
                jsonData = $"{{\"email\":\"{Email}\",\"passwordHash\":\"{passwordHash}\"}}";
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync("https://localhost:7033/login", content); // TODO BETTER LINKS

                if (!response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Error 3", responseContent, "OK");
                    return;
                }
                var jsonDocument = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                if (jsonDocument.RootElement.TryGetProperty("value", out var valueElement) && valueElement.ValueKind == JsonValueKind.String)
                {
                    string sessionId = valueElement.GetString();
                    await Application.Current.MainPage.Navigation.PushAsync(new MainMenuPage(sessionId)); // TODO NAVIGATION
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error 4", "SessionId for log in not recieved from server.", "OK"); // TODO NOTIFY
                }
            }
        }

        [RelayCommand]
        async Task NavigateToRegisterPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage()); // TODO NAVIGATION
        }
    }
}
