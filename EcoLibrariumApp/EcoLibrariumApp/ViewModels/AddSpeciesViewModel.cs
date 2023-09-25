using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoLibrariumApp.Models;
using EcoLibrariumApp.Services;
using System.Collections.ObjectModel;

namespace EcoLibrariumApp.ViewModels
{
    public partial class AddSpeciesViewModel : ObservableObject
    {
        [ObservableProperty]
        string latinName;

        [ObservableProperty]
        string commonName;

        [ObservableProperty]
        ObservableCollection<SpeciesProperty> propertyList = new ObservableCollection<SpeciesProperty>();

        [RelayCommand]
        public void AddProperty() {
            var property = new SpeciesProperty();
            property.Name = "";
            property.Value = "";

            PropertyList.Add(property);
        }
        [RelayCommand]
        public void RemoveProperty(SpeciesProperty item = null)
        {
            if(item == null)
            {
                return;
            }

            PropertyList.Remove(item);
        }

        [RelayCommand]
        public async Task SumbitSpeciesInfo()
        {
            Species species = new Species()
            {
                CommonName = CommonName,
                LatinName = LatinName,
                speciesProperties = PropertyList.ToList()
            };

            var result = await SpeciesService.AddSpecies(species);

            if(result.Success)
            {
                await MessageService.showMessage("Species added successfully.");
                await NavigationService.NavigateTo(new AdminMenuPage());
            } else
            {
                await MessageService.showMessage(result.Message);
            }
        }
    }
}
