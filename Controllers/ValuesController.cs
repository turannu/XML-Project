using Microsoft.AspNetCore.Mvc;
using SipsBites;
using XML_Project.Pages;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XML_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Brewery> Get()
        {
           
            return BreweryRepo.allBreweries;

            }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Brewery Get(int id)
        {

            return BreweryRepo.allBreweries[id];

        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Brewery brewery)
        {
            Console.WriteLine(brewery);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Brewery brewery)
        {
            Console.WriteLine(brewery);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Console.WriteLine(id);
        }
    }
}
