using EcoLibrariumServer.Data;
using EcoLibrariumServer.Middleware;
using EcoLibrariumServer.Models;
using EcoLibrariumServer.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoLibrariumServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ApiContext _context;

        public SpeciesController(ApiContext context)
        {
            _context = context;
        }


        [HttpPut("add")]
        public IActionResult AddSpecies([FromBody] Species species)
        {
            if (species == null)
            {
                return BadRequest("Species info not recieved.");
            }

            if (string.IsNullOrEmpty(species.LatinName) || string.IsNullOrEmpty(species.Name))
            {
                return BadRequest("Species latin or common name not specified.");
            }

            var speciesQuery = from s in _context.Species
                                   where (s.Name == species.Name || s.LatinName == species.LatinName)
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
            Species? species = _context.Species.Find(id);
            if(species == null)
            {
                return NotFound($"Species with the id {id} doesn't exist.");
            }

            return Ok(species);
        }


        [HttpGet("latin/{latingName}")]
        public IActionResult GetByLatinName(string latinName)
        {
            var speciesQuery = _context.Species.Where(a => a.Name.StartsWith(latinName, StringComparison.OrdinalIgnoreCase));

            if (speciesQuery.Count() <= 0)
            {
                return NotFound($"Species with the latin name {latinName} wasn't found.");
            }

            return Ok(speciesQuery.ToList());
        }

        [HttpGet("common/{commonName}")]
        public IActionResult GetByCommonName(string commonName)
        {
            var speciesQuery = _context.Species.Where(a => a.Name.StartsWith(commonName, StringComparison.OrdinalIgnoreCase));

            if (speciesQuery.Count() <= 0)
            {
                return NotFound($"Species with the common name {commonName} wasn't found.");
            }

            return Ok(speciesQuery.ToList());
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }


    }
}
