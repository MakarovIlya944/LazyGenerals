using System;
using System.Collections.Generic;
using System.Text;

namespace LazyGeneralsClient
{
    public class Game
    {
        public string Id { get; set; }
        public string Name;
        public string Password;
        public Client Host;
        public List<Client> Clients;
        public string GameStaff;
    }

    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Ready { get; set; } = false;
    }
}
