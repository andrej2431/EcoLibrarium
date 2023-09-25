using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoLibrariumApp.Models;
using EcoLibrariumApp.Services;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace EcoLibrariumApp.ViewModels
{
    public partial class SpeciesInfoViewModel : ObservableObject
    {
        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private bool noResultsFoundLabelVisible = true;

        [ObservableProperty]
        private bool searchResultsVisible = true;

        [ObservableProperty]
        private bool speciesInfoVisible = false;

        [ObservableProperty]
        private Species resultInfo;

        [ObservableProperty]
        private ObservableCollection<SpeciesProperty> resultProperties = new();



        [ObservableProperty]
        private ObservableCollection<Species> searchResults = new();

        async partial void OnSearchTextChanged(string value)
        {
            await Search();
        }

        public void ShowSearchResults()
        {
            SearchResultsVisible = true;
        }

        public void HideSearchResults()
        {
            SearchResultsVisible = false;
        }

        [RelayCommand]
        public async Task Search()
        {
            SearchResults.Clear();

            if (string.IsNullOrEmpty(SearchText)) {
                NoResultsFoundLabelVisible = true;
                return;
            }



            

            var response = await HttpService.GetRequest($"{HttpService.ApiUrls.SpeciesByCommon}/{SearchText}");
            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                NoResultsFoundLabelVisible = true;
                return;
            }

            NoResultsFoundLabelVisible = false;
            List<Species> speciesCollection = JsonConvert.DeserializeObject<List<Species>>(responseContent);

            foreach(Species species in speciesCollection)
            {
                SearchResults.Add(species);
            }
        }

        [RelayCommand]
        public void OpenSpeciesInfo(Species species)
        {
            ResultInfo = species;
            ResultProperties = new ObservableCollection<SpeciesProperty>(species.speciesProperties);
           
            SearchResultsVisible = false;
            SpeciesInfoVisible = true;
        }
    }
}