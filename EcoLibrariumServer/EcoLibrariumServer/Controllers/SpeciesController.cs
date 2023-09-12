using Microsoft.AspNetCore.Mvc;

namespace EcoLibrariumServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        // GET: api/<SpeciesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SpeciesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SpeciesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SpeciesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SpeciesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
