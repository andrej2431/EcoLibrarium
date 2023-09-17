
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Text;

namespace EcoLibrariumApp.Services
{
    internal class HttpService
    {
        public class ApiUrls
        {
            public const string BaseUrl = "https://localhost:7033";

            public const string Login = "/api/User/login";
            public const string Register = "/api/User/register";
            public const string Logout = "/api/User/logout";

            public const string SpeciesById = "/api/Species";
            public const string SpeciesByLatin = "/api/Species/latin";
            public const string SpeciesByCommon = "/api/Species/common";


        }


        private static HttpClient _httpClient;

        static HttpService()
        {
            _httpClient = new HttpClient();
        }

        public static async Task<HttpResponseMessage> PostRequest(string ApiUrl, dynamic data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            string sessionId = AuthenticationService.getSessionId();

            return await _httpClient.PostAsync($"{ApiUrls.BaseUrl}{ApiUrl}", content);
        }

        public static async Task<HttpResponseMessage> GetRequest(string ApiUrl)
        {
            return await _httpClient.GetAsync($"{ApiUrls.BaseUrl}{ApiUrl}");
        }

        public static void setSessionIdAuthHeader(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("sessionId", sessionId);
            }

        }

    }
}
