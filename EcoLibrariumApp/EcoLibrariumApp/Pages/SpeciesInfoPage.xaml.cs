using EcoLibrariumApp.ViewModels;

namespace EcoLibrariumApp;

public partial class SpeciesInfoPage : ContentPage
{
	private SpeciesInfoViewModel _viewModel;
	public SpeciesInfoPage()
	{
		InitializeComponent();
		_viewModel = new SpeciesInfoViewModel();
        BindingContext = _viewModel;
    }

	public void ShowSearchResults(object sender, EventArgs e)
	{
		_viewModel.ShowSearchResults();
	}

	public void HideSearchResults(object sender, EventArgs e)
	{
		_viewModel.HideSearchResults();
	}
}