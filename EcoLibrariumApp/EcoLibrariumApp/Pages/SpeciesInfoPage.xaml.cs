using EcoLibrariumApp.ViewModels;

namespace EcoLibrariumApp;

public partial class SpeciesInfoPage : ContentPage
{
	public SpeciesInfoPage()
	{
		InitializeComponent();

        BindingContext = new SpeciesInfoViewModel();
    }
}