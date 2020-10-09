using MongoDB.Driver;

namespace LazyGenerals.Server.Models
{
    public interface IDatabaseContext
    {
        IMongoDatabase MongoDatabase { get; }
    }
}