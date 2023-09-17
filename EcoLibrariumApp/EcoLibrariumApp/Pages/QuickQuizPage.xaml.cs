using EcoLibrariumApp.ViewModels;


namespace EcoLibrariumApp;

public partial class QuickQuizPage : ContentPage
{
	public QuickQuizPage()
	{
		InitializeComponent();
        BindingContext = new QuickQuizViewModel();
    }
}