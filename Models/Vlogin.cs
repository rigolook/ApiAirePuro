﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Airepuro.Api.Models
{
    public class Vlogin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Titulo")]
        public string Titulo { get; set; } = string.Empty;
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("UsuarioId")]
        public string UsuarioId { get; set; } = string.Empty;
        

        
        [BsonIgnore]
        public Usuario? Usuario { get; set; }
    }
}
