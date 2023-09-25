
namespace EcoLibrariumApp.Models
{
    public class UserInfo
    {
        public string Username { get; set; }
        
        public string Email { get; set; }

        public IList<string> Roles { get; set; }

        public bool isAdmin()
        {
            return Roles.Contains("Admin");
        }
    }
}
