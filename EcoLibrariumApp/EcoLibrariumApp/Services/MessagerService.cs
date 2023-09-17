
namespace EcoLibrariumApp.Services
{
    internal class MessageService



    {
        public MessageService() { }

        public static async Task showMessage(string message, string title="") {
            await Application.Current.MainPage.DisplayAlert(title,message, "OK");
        }
    }
}
