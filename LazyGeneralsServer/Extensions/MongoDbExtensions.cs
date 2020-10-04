#nullable enable
using System;
using MongoDB.Driver;

namespace LazyGenerals.Server.Extensions
{
    public static class MongoDbExtensions
    {
        public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase mongoDatabase, string? name = null, MongoCollectionSettings settings = null)
        {
            return mongoDatabase.GetCollection<T>(name ?? typeof(T).Name, settings);
        }

        public static bool Ping(this IMongoDatabase db, int secondsToWait = 1)
        {
            if (secondsToWait <= 0)
                throw new ArgumentOutOfRangeException(nameof(secondsToWait), secondsToWait, "Must be at least 1 second");

            return db.RunCommandAsync((Command<MongoDB.Bson.BsonDocument>)"{ping:1}").Wait(secondsToWait * 1000);
        }
    }
}