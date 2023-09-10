using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcoLibrariumApp;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    async void LoginButtonClicked(object sender, EventArgs args)
    {
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        string title = "Error";
        string message = "Make sure that username and password fields are not empty.";
        string cancel = "OK";

        if (username == null || password == null)
        {
            await DisplayAlert(title, message, cancel);
            return;
        }

        string passwordHash = EncryptUsingSha256(password);
        passwordHash = "string"; // TODO placeholder for testing

        try
        {
            using (HttpClient httpClient = new HttpClient())
            {

                string jsonData = $"{{\"id\":0,\"name\":\"{username}\",\"passwordHash\":\"{passwordHash}\",\"email\":\"andres2431@gmail.com\",\"admin\":false,\"sessionId\":\"string\"}}";
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7033/login", content);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    await DisplayAlert(title, await response.Content.ReadAsStringAsync(), cancel);
                }
                else
                {
                    await DisplayAlert(title,await response.Content.ReadAsStringAsync(), cancel);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(title, ex.Message, cancel);
        }
        return;
        await Navigation.PushAsync(new MainPage());
	}

	static string EncryptUsingSha256(string input)
	{
		using(SHA256 sha256 = SHA256.Create())
		{
			byte[] inputBytes = Encoding.UTF8.GetBytes(input);
			byte[] hashBytes = sha256.ComputeHash(inputBytes);
            string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();



			return hashString;
        }

    }
}