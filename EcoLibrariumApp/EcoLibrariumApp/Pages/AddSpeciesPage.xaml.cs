using EcoLibrariumApp.ViewModels;
using EcoLibrariumApp.Models;


namespace EcoLibrariumApp;

public partial class AddSpeciesPage : ContentPage
{
	public AddSpeciesPage()
	{
        BindingContext = new AddSpeciesViewModel();
        InitializeComponent();

		
		
	}
}