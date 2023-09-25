using EcoLibrariumApp.ViewModels;

namespace EcoLibrariumApp;

public partial class UploadQuizPage : ContentPage
{
	public UploadQuizPage()
	{
		InitializeComponent();

		BindingContext = new UploadQuizViewModel();
	}
}