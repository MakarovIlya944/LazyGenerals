using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LazyGeneralsServer.Models;
using LazyGeneralsServer.Models.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LazyGeneralsServer.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IServerContext _context;

        public GameController(IServerContext context)
        {
            _context = context;
        }

        // GET: api/<GameController>
        [HttpGet]
        public async Task<List<Game>> Get()
        {
            return await _context.GetAllClients();
        }

        // GET api/<GameController>/5
        [HttpGet("{name}")]
        public string Get(string name)
        {
            return "value";
        }

        // POST api/<GameController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GameController>/5
        [HttpPut("{name}")]
        public void CreateLobby(int name, [FromBody] string pass = "")
        {
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
