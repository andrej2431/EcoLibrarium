﻿
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Text;

namespace EcoLibrariumApp.Services
{
    internal class HttpService
    {
        public class ApiUrls
        {
            public const string BaseUrl = "https://localhost:7290";

            public const string Login = "/api/user/login";
            public const string Register = "/api/user/register";
            public const string Logout = "/api/user/logout";
            public const string UserInfo = "/api/user/userinfo";
            public const string Promote = "/api/user/promote";

            public const string SpeciesAll = "/api/species";
            public const string SpeciesById = "/api/species";
            public const string SpeciesByLatin = "/api/species/latin";
            public const string SpeciesByCommon = "/api/species/common";
            public const string SpeciesAdd = "/api/species";

            public const string QuickQuizzesAll = "/api/speciesquiz";
            public const string QuickQuizzesById = "/api/speciesquiz";
            public const string QuickQuizzesByName = "/api/speciesquiz/name";
            public const string QuickQuizzesAdd = "/api/speciesquiz";
            public const string QuickQuizzesUpload = "/api/speciesquiz";

            public const string QuizzesAll = "/api/quizcollection";
            public const string QuizzesById = "/api/quizcollection";
            public const string QuizzesByName = "/api/quizcollection/name";
            public const string QuizzesAdd = "/api/quizcollection";
            public const string QuizzesUpload = "/api/quizcollection";
        }

        private static HttpClientHandler _httpClientHandler;
        private static HttpClient _httpClient;
        private static CookieContainer _cookies;

        static HttpService()
        {
            
            _httpClientHandler = new HttpClientHandler();   
            _cookies = new CookieContainer();
            _httpClientHandler.CookieContainer = _cookies;
            _httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _httpClient = new HttpClient(_httpClientHandler);
        }

        public static async Task<HttpResponseMessage> PostRequest(string ApiUrl, dynamic data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync($"{ApiUrls.BaseUrl}{ApiUrl}", content);
        }

        public static async Task<HttpResponseMessage> GetRequest(string ApiUrl)
        {
            return await _httpClient.GetAsync($"{ApiUrls.BaseUrl}{ApiUrl}");
        }

        public static async Task<HttpResponseMessage> PutRequest(string ApiUrl, dynamic data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync($"{ApiUrls.BaseUrl}{ApiUrl}", content);
        }

        public static string? getCookie(string name)
        {
            var cookie = _cookies.GetCookies(new Uri(ApiUrls.BaseUrl)).Cast<Cookie>().FirstOrDefault(x => x.Name == name);
            return cookie?.Value;
        }

        public static void setCookie(string name, string value)
        {
            _cookies.SetCookies(new Uri(ApiUrls.BaseUrl), $"{name}={value}");

        }

    }
}
