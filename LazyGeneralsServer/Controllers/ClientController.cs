using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LazyGeneralsServer.Models;
using LazyGeneralsServer.Models.Entities;

namespace LazyGeneralsServer.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IServerContext _context;

        public ClientController(IServerContext context)
        {
            _context = context;
        }

        // GET: api/<ClientController>
        [HttpGet]
        public async Task<List<Client>> GetAll()
        {
            return await _context.GetAllClients();
        }

        [HttpGet("{name}")]
        public async Task<Client> Get(string name)
        {
            return await _context.GetClient(name);
        }

        [HttpPost]
        public CreatedResult Create(string name, string pass)
        {
            return Created("api/clients", _context.CreateClient(name, pass) );
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{name}")]
        public string Delete(string name)
        {
            var result = _context.DeleteClient(name);
            return result.ToString();
        }
    }
}
