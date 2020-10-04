using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LazyGenerals.Server.Models.Entities
{
    public class Client : IEntity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Display(Name = "Логин")]
        public string Name { get; set; }

        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public bool Ready { get; set; } = false;

        public override string ToString()
        {
            return "User: " + Name + (Ready ? " " : " not ") + "ready";
        }
    }
}
