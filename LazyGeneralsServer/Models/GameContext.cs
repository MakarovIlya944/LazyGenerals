using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LazyGenerals.Server.Extensions;
using LazyGenerals.Server.Models.Entities;
using LazyGenerals.Server.Models.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Linq;

namespace LazyGenerals.Server.Models
{

    public class GameContext : IGameContext
    {
        private readonly ILogger<GameContext> _logger;

        private readonly IMongoDatabase _database; // база данных
        private readonly IGridFSBucket _gridFS;   // файловое хранилище

        public GameContext(ILogger<GameContext> logger, IOptions<MongoDBOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            MongoInternalIdentity internalIdentity = new MongoInternalIdentity("admin", options.Value.Username);
            PasswordEvidence passwordEvidence = new PasswordEvidence(options.Value.Password);
            MongoCredential mongoCredential = new MongoCredential(options.Value.AuthMechanism, internalIdentity, passwordEvidence);
            MongoServerAddress addres = new MongoServerAddress(options.Value.ConnectionString);

            MongoClientSettings settings = new MongoClientSettings() { Credential = mongoCredential, Server = addres };

            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(settings);
            // получаем доступ к самой базе данных
            _database = client.GetDatabase(options.Value.DatabaseName);
            // получаем доступ к файловому хранилищу
            _gridFS = new GridFSBucket(_database);

            try
            {
                _database.Ping();
            }
            catch (Exception)
            {
                _logger.LogError("Failed to connect to Mongo _database!");
                throw;
            }
        }

        public IEnumerable<Game> GetAllGames()
        {
            var games = _database.GetCollection<Game>().Find(_ => true).ToList();
            games.ForEach(x => x.Password = x.Password == "" ? "" : "***");
            return games;
        }

        public Task<Game> GetGame(string name)
        {
            return _database.GetCollection<Game>().Find(c => c.Name == name).FirstOrDefaultAsync();
        }

        public Game CreateGame(string name, string pass, Client host, string gameStaff)
        {
            // TODO add check exist
            _database.GetCollection<Game>().InsertOne(new Game()
            {
                Name = name,
                Password = pass,
                Host = host,
                GameStaff = gameStaff,
                Clients = new List<Client>()
            });
            var game = _database.GetCollection<Game>().Find(c => c.Name == name).FirstOrDefault();
            game.Password = "***";
            return game;
        }

        public DeleteResult DeleteGame(string name)
        {
            return _database.GetCollection<Game>().DeleteOne(c => c.Name == name);
        }
    }
}