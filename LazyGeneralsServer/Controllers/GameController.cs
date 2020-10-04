using System.Collections.Generic;
using System.Threading.Tasks;
using LazyGenerals.Server.Models;
using LazyGenerals.Server.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LazyGenerals.Server.Controllers
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

        [HttpGet]
        public IEnumerable<Game> Get()
        {
            return  _context.GetAllGames();
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<Game> Get(string name)
        {
            return _context.GetGame(name);
        }

        [HttpPost("{name}")]
        public async Task<CreatedResult> CreateLobby(string name, string client, string pass = "", string comment = "")
        {
            Client c = await _context.GetClient(client);
            return Created("api/{name}", _context.CreateGame(name, pass, c, comment));
        }

        [HttpDelete("{name}")]
        public string Delete(string name)
        {
            var result = _context.DeleteGame(name);
            return result.ToString();
        }
    }
}
