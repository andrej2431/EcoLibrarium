using EcoLibrariumApp.ViewModels;


namespace EcoLibrariumApp;

public partial class SavedQuizzesPage : ContentPage
{
	public SavedQuizzesPage()
	{
		InitializeComponent();
        BindingContext = new SavedQuizzesViewModel();
    }
}