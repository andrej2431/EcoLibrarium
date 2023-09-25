using EcoLibrariumApp.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;


namespace EcoLibrariumApp.Services
{
    internal class SpeciesService
    {
        public struct SpeciesResult {
            public SpeciesResult(bool success, string message)
            {
                Success = success;
                Message = message;
            }
            public bool Success;
            public string Message;
        }

        public SpeciesService() { }

        public static async Task<SpeciesResult> AddSpecies(Species species) { 
            if(string.IsNullOrEmpty(species.CommonName) || string.IsNullOrEmpty(species.LatinName)) {
                return new SpeciesResult(false, "Both latin name and common name are required.");
            }

            HttpResponseMessage response = await HttpService.PutRequest(HttpService.ApiUrls.SpeciesAdd, species);

            if(!response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return new SpeciesResult(false, $"error code {response.StatusCode}, content: {responseContent}");
            }

            return new SpeciesResult(true, "Species successfully added");
        }
    }
}