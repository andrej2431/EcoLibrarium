using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoLibrariumApp.Services;
using System.Text;
using System.Text.Json;

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
            if (Email == null || Password == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error 1", "Empty username or password.", "OK"); // TODO NOTIFY
                return;
            }

            string passwordHash = EncryptionService.EncryptUsingSha256(Password);

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string jsonData = $"{{\"email\":\"{Email}\",\"passwordHash\":\"{passwordHash}\"}}";
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7033/login", content); // TODO BETTER LINKS
                    
                    string responseContent = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Error 2", responseContent, "OK"); // TODO NOTIFY

                    if(!response.IsSuccessStatusCode)
                    {
                        return;
                    }

                    //if (!response.IsSuccessStatusCode)
                    //{
                    //    responseContent = await response.Content.ReadAsStringAsync();
                    //    await Application.Current.MainPage.DisplayAlert("Error 2", responseContent, "OK"); // TODO NOTIFY
                    //    return;
                    //}

                    

                    var jsonDocument = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                    if (jsonDocument.RootElement.TryGetProperty("value", out var valueElement) && valueElement.ValueKind == JsonValueKind.String)
                    {
                        string sessionId = valueElement.GetString();

                        await Application.Current.MainPage.Navigation.PushAsync(new MainMenuPage(sessionId)); // TODO NAVIGATION
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error 3", "SessionId for log in not recieved from server.", "OK"); // TODO NOTIFY
                    }
                }


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error 4", "Exception: " + ex.Message, "OK"); // TODO NOTIFY
            }

        }

        [RelayCommand]
        async Task NavigateToRegisterPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage()); // TODO NAVIGATION
        }

    }
}