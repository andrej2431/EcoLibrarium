
namespace EcoLibrariumApp.Models
{
    public class Species
    {
        public int Id { get; set; }

        public string LatinName { get; set; }

        public string CommonName { get; set; }

        public List<SpeciesProperty> speciesProperties { get; set; }
    }

    public class SpeciesProperty
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
