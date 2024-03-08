using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Airepuro.Api.Models
{
    public class Ventilador
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("RPM")]
        public int RPM { get; set; }
        [BsonElement("Ubicacion")]
        public string Ubicacion { get; set; } = string.Empty;
        [BsonElement("Encendido")]
        public bool Encendido { get; set; }

    }
}
