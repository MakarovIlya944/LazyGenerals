using LazyGeneralsServer.Models.Entities;
using LazyGeneralsServer.Models.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace LazyGeneralsServer.Models
{
    public interface IServerContext
    {
        IEnumerable<Client> GetAllClients();
        Task<Client> GetClient(string name);
        Client CreateClient(string username, string pass);
        ReplaceOneResult ChangePassword(string username, string newPass);
        DeleteResult DeleteClient(string username);

        IEnumerable<Game> GetAllGames();
        Task<Game> GetGame(string name);
        Game CreateGame(string name, string pass, Client host, string gameStaff);
        DeleteResult DeleteGame(string name);
    }

    public class ServerContext : IServerContext
    {
        private IMongoDatabase database; // база данных
        private IGridFSBucket gridFS;   // файловое хранилище

        public ServerContext(IOptions<MongoDBOptions> options)
        {
            MongoInternalIdentity internalIdentity = new MongoInternalIdentity("admin", options.Value.Username);
            PasswordEvidence passwordEvidence = new PasswordEvidence(options.Value.Password);
            MongoCredential mongoCredential = new MongoCredential(options.Value.AuthMechanism, internalIdentity, passwordEvidence);
            MongoServerAddress addres = new MongoServerAddress(options.Value.ConnectionString);

            MongoClientSettings settings = new MongoClientSettings() { Credential = mongoCredential, Server = addres };

            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(settings);
            // получаем доступ к самой базе данных
            database = client.GetDatabase(options.Value.DatabaseName);
            // получаем доступ к файловому хранилищу
            gridFS = new GridFSBucket(database);
        }

        public IEnumerable<Client> GetAllClients()
        {
            var clients = database.GetCollection<Client>("Client").Find(_ => true).ToList();
            clients.ForEach(x => x.Password = x.Password == "" ? "" : "***");
            return clients;
        }

        public Task<Client> GetClient(string username)
        {
            return database.GetCollection<Client>("Client").Find(c => c.Name == username).FirstOrDefaultAsync();
        }

        public ReplaceOneResult ChangePassword(string username, string newPass)
        {
            return database.GetCollection<Client>("Client").ReplaceOne(c => c.Name == username, new Client() { Name = username, Password = newPass });
        }

        public DeleteResult DeleteClient(string username)
        {
            return database.GetCollection<Client>("Client").DeleteOne(c => c.Name == username);
        }

        public Client CreateClient(string username, string pass)
        {
            // TODO add check exist
            database.GetCollection<Client>("Client").InsertOne(new Client() { Name = username, Password = pass, Ready = false });
            var client = database.GetCollection<Client>("Client").Find(c => c.Name == username).FirstOrDefault();
            client.Password = "***";
            return client;
        }

        public IEnumerable<Game> GetAllGames()
        {
            var games = database.GetCollection<Game>("Game").Find(_ => true).ToList();
            games.ForEach(x => x.Password = x.Password == "" ? "" : "***");
            return games;
        }

        public Task<Game> GetGame(string name)
        {
            return database.GetCollection<Game>("Game").Find(c => c.Name == name).FirstOrDefaultAsync();
        }

        public Game CreateGame(string name, string pass, Client host, string gameStaff)
        {
            // TODO add check exist
            database.GetCollection<Game>("Game").InsertOne(new Game() {
                Name = name, Password = pass, Host = host, GameStaff = gameStaff, Clients = new List<Client>() 
            });
            var game = database.GetCollection<Game>("Game").Find(c => c.Name == name).FirstOrDefault();
            game.Password = "***";
            return game;
        }

        public DeleteResult DeleteGame(string name)
        {
            return database.GetCollection<Game>("Game").DeleteOne(c => c.Name == name);
        }
    }
}