using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LazyGenerals.Server.Models.Entities;
using LazyGenerals.Server.Extensions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Linq;

namespace LazyGenerals.Server.Models
{
    public class ClientContext : IClientContext
    {
        private readonly ILogger<ClientContext> _logger;
        private readonly IMongoDatabase _mongoDatabase; // база данных
        private readonly IGridFSBucket _gridFS;   // файловое хранилище
        private readonly IGameContext gameContext;

        public ClientContext(ILogger<ClientContext> logger, IDatabaseContext database)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mongoDatabase = database.MongoDatabase;
        }

        public IEnumerable<Client> GetAllClients()
        {
            var clients = _mongoDatabase.GetCollection<Client>("Client").Find(_ => true).ToList();
            clients.ForEach(x => x.Password = x.Password == "" ? "" : "***");
            return clients;
        }

        public Task<Client> GetClient(string username)
        {
            return _mongoDatabase.GetCollection<Client>().Find(c => c.Name == username).FirstOrDefaultAsync();
        }

        public ReplaceOneResult ChangePassword(string username, string newPass)
        {
            return _mongoDatabase.GetCollection<Client>().ReplaceOne(c => c.Name == username, new Client() { Name = username, Password = newPass });
        }

        public DeleteResult DeleteClient(string username)
        {
            return _mongoDatabase.GetCollection<Client>().DeleteOne(c => c.Name == username);
        }

        public Client CreateClient(string username, string pass)
        {
            _mongoDatabase.GetCollection<Client>().InsertOne(new Client() { Name = username, Password = pass, Ready = false });
            var client = _mongoDatabase.GetCollection<Client>().Find(c => c.Name == username).FirstOrDefault();
            client.Password = "***";
            return client;
        }
    }
}