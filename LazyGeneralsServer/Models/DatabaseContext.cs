using System;
using LazyGenerals.Server.Models.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using LazyGenerals.Server.Extensions;

namespace LazyGenerals.Server.Models
{

    public class DatabaseContext : IDatabaseContext
    {
        private readonly ILogger<DatabaseContext> _logger;
        private readonly IMongoDatabase _mongoDatabase;

        public IMongoDatabase MongoDatabase => _mongoDatabase;
        public DatabaseContext(ILogger<DatabaseContext> logger, IOptions<MongoDBOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            MongoInternalIdentity internalIdentity = new MongoInternalIdentity("admin", options.Value.Username);
            PasswordEvidence passwordEvidence = new PasswordEvidence(options.Value.Password);
            MongoCredential mongoCredential = new MongoCredential(options.Value.AuthMechanism, internalIdentity, passwordEvidence);
            MongoServerAddress address = new MongoServerAddress(options.Value.ConnectionString);

            MongoClientSettings settings = new MongoClientSettings() { Credential = mongoCredential, Server = address };

            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(settings);
            // получаем доступ к самой базе данных
            _mongoDatabase = client.GetDatabase(options.Value.DatabaseName);

            try
            {
                _mongoDatabase.Ping();
            }
            catch (Exception)
            {
                _logger.LogError("Failed to connect to Mongo _database!");
                throw;
            }
        }
    }
}