using System.ComponentModel.DataAnnotations;

namespace EcoLibrariumServer.Models.Authentication
{
    public class LoginUserCredentials
    {
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
