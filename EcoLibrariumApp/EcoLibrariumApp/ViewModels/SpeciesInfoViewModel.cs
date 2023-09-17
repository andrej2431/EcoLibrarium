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
        private ObservableCollection<string> searchResults = new ObservableCollection<string>();

        async partial void OnSearchTextChanged(string value)
        {
            await Search();
        }

        [RelayCommand]
        public async Task Search()
        {
            if (string.IsNullOrEmpty(SearchText)) { return; }

            SearchResults.Clear();
            

            var response = await HttpService.GetRequest($"{HttpService.ApiUrls.SpeciesByCommon}/{SearchText}");
            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                SearchResults.Add($"Search failed! {responseContent}");
                return;
            }

            List<Species> speciesCollection = JsonConvert.DeserializeObject<List<Species>>(responseContent);

            foreach(Species species in speciesCollection)
            {
                
                SearchResults.Add(species.Name);
            }
        }
    }
}