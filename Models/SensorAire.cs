using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Airepuro.Api.Models
{
    public class SensorAire
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("GasDetectado")]
        public string GasDetectado { get; set; } = string.Empty;
        [BsonElement("Ubicacion")]
        public string Ubicacion { get; set; } = string.Empty;
        [BsonElement("NombreGas")]
        public string NombreGas { get; set; } = string.Empty;

    }
}