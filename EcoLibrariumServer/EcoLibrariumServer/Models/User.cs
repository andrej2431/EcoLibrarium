using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcoLibrariumServer.Models
{
    [Index(nameof(User.Name), IsUnique = true)]
    [Index(nameof(User.Email),IsUnique = true)]
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(15,MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        public bool Admin {  get; set; }
        public string SessionId { get; set; }

    }
}
