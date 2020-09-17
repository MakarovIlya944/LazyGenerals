using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LazyGeneralsServer.Models.Entities
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Display(Name = "Имя")]
        public string Name;
        [Display(Name = "Пароль")]
        public string Password;
        [Display(Name = "Хост")]
        public Client Host;
        [Display(Name = "Клиенты")]
        public List<Client> Clients;
        [Display(Name = "Описание игры")]
        public string GameStaff;

        //public override string ToString()
        //{
        //    return (IsPrivate ? "Private" : "Public") +  $" {Name}\tClient:{Client}\t" + (IsPlaying ? "open" : "closed");
        //}
    }
}
