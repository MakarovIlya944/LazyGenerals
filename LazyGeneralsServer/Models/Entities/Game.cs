﻿using MongoDB.Bson;
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
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Хост")]
        public Client Host { get; set; }
        [Display(Name = "Клиенты")]
        public List<Client> Clients { get; set; }
        [Display(Name = "Описание игры")]
        public string GameStaff { get; set; }

        public override string ToString()
        {
            return $"{Name}: {Host}" + Password == "" ? "" : " need pass";
        }
    }
}
