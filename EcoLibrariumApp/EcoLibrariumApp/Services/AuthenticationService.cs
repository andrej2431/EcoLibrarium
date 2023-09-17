using EcoLibrariumApp.Models;
using Newtonsoft.Json;
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
        class HttpResponseUser
        {
            public string username = null;

            public string email = null;

            public string sessionId = null;
        }

        private static User user;

        public AuthenticationService() { }

        public static async Task<AuthenticationResult> LogIn(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new AuthenticationResult(false, "Empty email or password.");
            }

            string passwordHash = EncryptionService.EncryptUsingSha256(password);

            try
            {
                var credentials = new { email = email, passwordHash = passwordHash };
                HttpResponseMessage response = await HttpService.PostRequest(HttpService.ApiUrls.Login, credentials);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new AuthenticationResult(false, responseContent);
                }
                

                SetUserFromLoginResponse(responseContent);

                return user != null ?
                    new AuthenticationResult(true, "Login successful.") :
                    new AuthenticationResult(false, "Recieving user info from login response failed.");
            }
            catch (Exception ex)
            {
                return new AuthenticationResult(false, $"Error: {ex.Message}");
            }
        }

        private static void SetUserFromLoginResponse(string responseContent)
        {
            HttpResponseUser responseUser = JsonConvert.DeserializeObject<HttpResponseUser>(responseContent);

            if (string.IsNullOrEmpty(responseUser.username) ||
                string.IsNullOrEmpty(responseUser.email) ||
                string.IsNullOrEmpty(responseUser.sessionId))
            {
                user = null;
            }
            else
            {
                user = new User();
                user.Username = responseUser.username;
                user.Email = responseUser.email;
                user.SessionId = responseUser.sessionId;

                HttpService.setSessionIdAuthHeader(user.SessionId);
            }


        }

        public static async Task<AuthenticationResult> Register(string username, string email, string password)
        {


            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new AuthenticationResult(false, "Username, email and password cannot be empty.");
            }

            string passwordHash = EncryptionService.EncryptUsingSha256(password);

            try
            {
                var credentials = new {name = username, email = email, passwordHash = passwordHash};
                HttpResponseMessage response = await HttpService.PostRequest(HttpService.ApiUrls.Register, credentials);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
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

        public static string getSessionId()
        {
            if (user == null || user.SessionId == null) { return null; }

            return user.SessionId;
        }

        public static async Task<AuthenticationResult> Logout()
        {
            

            if (user == null)
            {
                HttpService.setSessionIdAuthHeader(null);
                return new AuthenticationResult(true, "User already logged out.");
            }

            if (string.IsNullOrEmpty(user.SessionId))
            {
                user = null;
                HttpService.setSessionIdAuthHeader(null);
                return new AuthenticationResult(true, "User already logged out, but removed artifacts.");
            }


            try
            {
                var sessionInfo = new {sessionId =  user.SessionId};
                HttpResponseMessage response = await HttpService.PostRequest(HttpService.ApiUrls.Logout, sessionInfo);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new AuthenticationResult(false, responseContent);
                }

                user = null;
                HttpService.setSessionIdAuthHeader(null);
                return new AuthenticationResult(true, "Successful logout");
            }
            catch (Exception ex)
            {
                return new AuthenticationResult(false, $"Error: {ex.Message}");
            }




        }

        public static bool IsLoggedIn()
        {
            return user != null && !string.IsNullOrEmpty(user.SessionId);
        }
    }
}

