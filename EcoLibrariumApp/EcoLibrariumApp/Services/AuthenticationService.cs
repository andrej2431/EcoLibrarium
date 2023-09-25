using EcoLibrariumApp.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;


namespace EcoLibrariumApp.Services
{
    struct AuthenticationResult
    {
        public AuthenticationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public bool Success;
        public string Message;
    }



    internal class AuthenticationService
    {
        private static UserInfo _userInfo;

        public AuthenticationService() { }

        public static async Task<AuthenticationResult> LogIn(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new AuthenticationResult(false, "Empty email or password.");
            }

            try
            {
                var credentials = new { email = email, password = password };

                HttpResponseMessage response = await HttpService.PostRequest(HttpService.ApiUrls.Login, credentials);

                if (!response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return new AuthenticationResult(false, responseContent);
                }


                HttpResponseMessage userInfoResponse = await HttpService.GetRequest(HttpService.ApiUrls.UserInfo);
                if(!userInfoResponse.IsSuccessStatusCode)
                {
                    await HttpService.PostRequest(HttpService.ApiUrls.Logout, null);
                    return new AuthenticationResult(false, userInfoResponse.StatusCode.ToString());
                }


                string userInfoSerialized = await userInfoResponse.Content.ReadAsStringAsync();

                _userInfo = JsonConvert.DeserializeObject<UserInfo>(userInfoSerialized);
                return new AuthenticationResult(true, "Login successful.");
            }
            catch (Exception ex)
            {
                return new AuthenticationResult(false, $"Error: {ex.Message}");
            }
        }


        public static async Task<AuthenticationResult> Register(string username, string email, string password)
        {


            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new AuthenticationResult(false, "Username, email and password cannot be empty.");
            }


            try
            {
                var credentials = new { username = username, email = email, password = password };
                HttpResponseMessage response = await HttpService.PostRequest(HttpService.ApiUrls.Register, credentials);


                if (!response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return new AuthenticationResult(false, responseContent);
                }
                else
                {
                    return new AuthenticationResult(true, "Registration successful.");
                }
            }
            catch (Exception ex)
            {
                return new AuthenticationResult(false, $"Error : {ex.Message}");
            }



        }



        public static async Task<AuthenticationResult> Logout()
        {

            HttpResponseMessage response = await HttpService.PostRequest(HttpService.ApiUrls.Logout, null);


            if (!response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return new AuthenticationResult(false, responseContent);
            }
            else
            {
                _userInfo = null;
                return new AuthenticationResult(true, "Successful logout");
            }

        }

        public static async Task<AuthenticationResult> PromoteUser(UserPromotionInfo userPromotionInfo)
        {
            if (!IsAdmin())
            {
                return new AuthenticationResult(false, "You have to be logged in as an admin to do promote a user.");
            }

            HttpResponseMessage response = await HttpService.PostRequest(HttpService.ApiUrls.Promote, userPromotionInfo);

            if(!response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return new AuthenticationResult(false, responseContent);
            }

            return new AuthenticationResult(true, "User promoted successfully.");
        }

        public static bool IsLoggedIn()
        {
            return _userInfo != null;
        }

        public static bool IsAdmin()
        {

            return IsLoggedIn() && _userInfo.isAdmin();
        }

        
    }
}

