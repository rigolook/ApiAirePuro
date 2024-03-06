using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace Airepuro.Api.Models
{
    public class SensorTemperatura
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Temperatura")]
        public int Temperatura { get; set; }
        [BsonElement("Ubicacion")]
        public string Ubicacion { get; set; } = string.Empty;
        [BsonElement("Humedad")]
        public string Humedad { get; set; } = string.Empty;
    }
}
