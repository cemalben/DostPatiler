using DostPatiler.DA;
using DostPatiler.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DostPatiler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HayvanApiController : ControllerBase
    {
        private readonly IHayvanRepo hayvan;

        public HayvanApiController(IHayvanRepo a)
        {
            this.hayvan = a;
        }

        // GET: api/<HayvanApiController>
        [HttpGet]
        public List<Hayvan> Get()
        {
            return hayvan.GetAnimals();
        }

        // GET api/<HayvanApiController>/5
        [HttpGet("{id}")]
        public Hayvan Get(int id)
        {
            return hayvan.GetAnimalById(id);
        }

        // POST api/<HayvanApiController>
        [HttpPost]
        public Hayvan Post([FromBody] Hayvan value)
        {
            return hayvan.CreateAnimal(value);
        }

        // PUT api/<HayvanApiController>/5
        [HttpPut("{id}")]
        public Hayvan Put(int id, [FromBody] Hayvan value)
        {
            return hayvan.UpdateAnimal(id, value);
        }

        // DELETE api/<HayvanApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            hayvan.DeleteAnimal(id);
        }
    }
}
