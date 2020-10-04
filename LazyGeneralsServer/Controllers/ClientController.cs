using System.Collections.Generic;
using System.Threading.Tasks;
using LazyGenerals.Server.Models;
using LazyGenerals.Server.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LazyGenerals.Server.Controllers
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

        [HttpGet]
        public IEnumerable<Client> GetAll()
        {
            return _context.GetAllClients();
        }

        [HttpGet("{name}")]
        public async Task<Client> Get(string name)
        {
            return await _context.GetClient(name);
        }

        [HttpPost("{name}")]
        public CreatedResult Create(string name, string pass)
        {
            return Created("api/clients", _context.CreateClient(name, pass) );
        }

        [HttpDelete("{name}")]
        public string Delete(string name)
        {
            var result = _context.DeleteClient(name);
            return result.ToString();
        }
    }
}
