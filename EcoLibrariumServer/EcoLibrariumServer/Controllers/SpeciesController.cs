using EcolibrariumServer.Models.Species;
using EcoLibrariumServer.Data;
using EcoLibrariumServer.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoLibrariumServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ApiContext _context;

        public SpeciesController(ApiContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("")]
        public IActionResult AddSpecies([FromBody] Species species)
        {
            
            if (species == null)
            {
                return BadRequest("Species info not recieved.");
            }

            if (string.IsNullOrEmpty(species.LatinName) || string.IsNullOrEmpty(species.CommonName))
            {
                return BadRequest("Species latin or common name not specified.");
            }

            var speciesQuery = from s in _context.Species
                                   where (s.CommonName == species.CommonName || s.LatinName == species.LatinName)
                                   select s;

            if (speciesQuery.Any())
            {
                return BadRequest("Species with that latin or common name already exists");
            }

            _context.Species.Add(species);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var speciesQuery = _context.Species.Where(species => species.Id == id);
            if(speciesQuery.Count() <= 0)
            {
                return NotFound($"Species with the id {id} doesn't exist.");
            }

            return Ok(speciesQuery.Include(species=>species.speciesProperties).ToList());
        }


        [HttpGet("latin/{latinName}")]
        public IActionResult GetByLatinName(string latinName)
        {
            var speciesQuery = _context.Species.Where(species => species.CommonName.StartsWith(latinName, StringComparison.OrdinalIgnoreCase));

            if (speciesQuery.Count() <= 0)
            {
                return NotFound($"Species with the latin name {latinName} wasn't found.");
            }

            return Ok(speciesQuery.Include(species => species.speciesProperties).ToList());
        }

        [HttpGet("common/{commonName}")]
        public IActionResult GetByCommonName(string commonName)
        {
            var speciesQuery = _context.Species.Where(species => species.CommonName.StartsWith(commonName, StringComparison.OrdinalIgnoreCase));

            if (speciesQuery.Count() <= 0)
            {
                return NotFound($"Species with the common name {commonName} wasn't found.");
            }

            return Ok(speciesQuery.Include(species => species.speciesProperties).ToList());
        }
    }
}
