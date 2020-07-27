using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LazyGeneralsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        // GET: api/<LobbyController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LobbyController>/5
        [HttpGet("{name}")]
        public string Get(string name)
        {
            return "value";
        }

        // POST api/<LobbyController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LobbyController>/5
        [HttpPut("{name}")]
        public void CreateLobby(int name, [FromBody] string pass = "")
        {
        }

        // DELETE api/<LobbyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
