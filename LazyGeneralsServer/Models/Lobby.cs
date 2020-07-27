using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazyGeneralsServer.Models.Lobby
{
    public class Lobby
    {
        public string Name;
        public string Password;
        public bool IsPrivate = false;
        public bool IsPlaying = false;
        public Guid Client;

        public Lobby(string name, Guid client)
        {
            Name = name;
            Password = "";
            Client = client;
        }

        public Lobby(string name, string pass, Guid client)
        {
            Name = name;
            Password = pass;
            Client = client;
            IsPrivate = true;
        }

        public override string ToString()
        {
            return (IsPrivate ? "Private" : "Public") +  $" {Name}\tClient:{Client}\t" + (IsPlaying ? "open" : "closed");
        }
    }
}
