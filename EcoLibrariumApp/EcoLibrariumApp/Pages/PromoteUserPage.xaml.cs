using EcoLibrariumApp.ViewModels;

namespace EcoLibrariumApp;

public partial class PromoteUserPage : ContentPage
{
	public PromoteUserPage()
	{
		InitializeComponent();

        BindingContext = new PromoteUserViewModel();
    }
}