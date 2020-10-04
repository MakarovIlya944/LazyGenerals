using System.Collections.Generic;

namespace LazyGenerals.Client.Models
{
    public class Game
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public Client Host { get; set; }

        public List<Client> Clients { get; set; }

        public string GameStaff { get; set; }
    }
}