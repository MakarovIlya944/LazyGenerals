namespace LazyGeneralsServer.Models.Options
{
    public class MongoDBOptions : IMongoDBOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AuthMechanism { get; set; }
    }

    public interface IMongoDBOptions
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AuthMechanism { get; set; }
    }
}