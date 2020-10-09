using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LazyGenerals.Server.Extensions;
using LazyGenerals.Server.Models.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Linq;

namespace LazyGenerals.Server.Models
{

    public class GameContext : IGameContext
    {
        private readonly ILogger<GameContext> _logger;

        private readonly IMongoDatabase _mongoDatabase; // база данных
        private readonly IGridFSBucket _gridFS;   // файловое хранилище

        public GameContext(ILogger<GameContext> logger, IDatabaseContext database)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mongoDatabase = database.MongoDatabase;
        }

        public IEnumerable<Game> GetAllGames()
        {
            var games = _mongoDatabase.GetCollection<Game>().Find(_ => true).ToList();
            games.ForEach(x => x.Password = x.Password == "" ? "" : "***");
            return games;
        }

        public Task<Game> GetGame(string name)
        {
            return _mongoDatabase.GetCollection<Game>().Find(c => c.Name == name).FirstOrDefaultAsync();
        }

        public Game CreateGame(string name, string pass, Client host, string gameStaff)
        {
            _mongoDatabase.GetCollection<Game>().InsertOne(new Game()
            {
                Name = name,
                Password = pass,
                Host = host,
                GameStaff = gameStaff,
                Clients = new List<Client>()
            });
            var game = _mongoDatabase.GetCollection<Game>().Find(c => c.Name == name).FirstOrDefault();
            game.Password = "***";
            return game;
        }

        public DeleteResult DeleteGame(string name)
        {
            return _mongoDatabase.GetCollection<Game>().DeleteOne(c => c.Name == name);
        }
    }
}