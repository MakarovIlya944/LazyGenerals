using System;
using System.Collections.Generic;
using System.Text;

namespace LazyGeneralsClient
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

    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Ready { get; set; } = false;
    }
}
