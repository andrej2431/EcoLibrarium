using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace EcolibrariumServer.Models.Species
{
    public class Species
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? LatinName { get; set; }

        [Required]
        [StringLength(100)]
        public string? CommonName { get; set; }


        public List<SpeciesProperty> speciesProperties { get; set; } = new();

    }

    public class SpeciesProperty
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;

        public string Name { get; set; } = "";

        public string Value { get; set; } = "";
    }
}