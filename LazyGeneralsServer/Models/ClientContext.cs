using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LazyGenerals.Server.Models.Entities;
using LazyGenerals.Server.Extensions;
using LazyGenerals.Server.Models.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Linq;

namespace LazyGenerals.Server.Models
{
    public class ClientContext : IClientContext
    {
        private readonly ILogger<ClientContext> _logger;

        private readonly IMongoDatabase _database; // база данных
        private readonly IGridFSBucket _gridFS;   // файловое хранилище
        private readonly IGameContext gameContext;

        public ClientContext(ILogger<ClientContext> logger, IOptions<MongoDBOptions> options)
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
                GetAllClients();
            }
            catch (Exception)
            {
                _logger.LogError("Failed to connect to Mongo _database!");
                throw;
            }
        }

        public IEnumerable<Client> GetAllClients()
        {
            var clients = _database.GetCollection<Client>("Client").Find(_ => true).ToList();
            clients.ForEach(x => x.Password = x.Password == "" ? "" : "***");
            return clients;
        }

        public Task<Client> GetClient(string username)
        {
            return _database.GetCollection<Client>().Find(c => c.Name == username).FirstOrDefaultAsync();
        }

        public ReplaceOneResult ChangePassword(string username, string newPass)
        {
            return _database.GetCollection<Client>().ReplaceOne(c => c.Name == username, new Client() { Name = username, Password = newPass });
        }

        public DeleteResult DeleteClient(string username)
        {
            return _database.GetCollection<Client>().DeleteOne(c => c.Name == username);
        }

        public Client CreateClient(string username, string pass)
        {
            // TODO add check exist
            _database.GetCollection<Client>().InsertOne(new Client() { Name = username, Password = pass, Ready = false });
            var client = _database.GetCollection<Client>().Find(c => c.Name == username).FirstOrDefault();
            client.Password = "***";
            return client;
        }
    }
}