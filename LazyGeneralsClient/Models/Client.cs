namespace LazyGenerals.Client.Models
{
    public class Client
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public bool Ready { get; set; } = false;
    }
}