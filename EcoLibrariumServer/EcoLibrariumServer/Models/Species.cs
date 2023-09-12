using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EcoLibrariumServer.Models
{
    [Index(nameof(Species.LatinName), IsUnique = true)]
    public class Species
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string LatinName { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public Dictionary<string, string> Data;
    }

}