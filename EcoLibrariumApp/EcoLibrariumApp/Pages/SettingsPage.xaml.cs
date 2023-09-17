using EcoLibrariumApp.ViewModels;

namespace EcoLibrariumApp;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
        BindingContext = new SettingsViewModel();
    }
}