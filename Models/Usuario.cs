using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Airepuro.Api.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Nombre")]
        public string Nombre { get; set; } = string.Empty;
        [BsonElement("Contrasena")]
        public string Contrasena { get; set; } = string.Empty;
        [BsonElement("Numero")]
        public Int64 Numero { get; set; }
    }
}
