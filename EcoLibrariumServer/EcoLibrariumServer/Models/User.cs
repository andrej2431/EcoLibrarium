using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcoLibrariumServer.Models
{
    [Index(nameof(User.Email),IsUnique = true)]
    public class User
    {
        public User(string name, string email, string passwordHash) {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;

            Admin = false;
            SessionId = "";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(15,MinimumLength = 5)]
        public string  Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool Admin {  get; set; }

        public string SessionId { get; set; }

    }

    public class RegisterUserCredentials
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }

    public class LoginUserCredentials
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
