using System.Collections.Generic;
using System.Threading.Tasks;
using LazyGenerals.Server.Models.Entities;
using MongoDB.Driver;

namespace LazyGenerals.Server.Models
{
    public interface IGameContext
    {
        Game CreateGame(string name, string pass, Client host, string gameStaff);
        IEnumerable<Game> GetAllGames();
        Task<Game> GetGame(string name);
        DeleteResult DeleteGame(string name);
    }
}