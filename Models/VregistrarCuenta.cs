using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Airepuro.Api.Models
{
    public class VregistrarCuenta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Titulo")]
        public string Titulo { get; set; } = string.Empty;

        [BsonElement("Mensaje")]
        public string Mensaje { get; set; } = string.Empty;

        [BsonElement("UsuarioId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UsuarioId { get; set; } = string.Empty;

        [BsonIgnore]
        public Usuario? Usuario { get; set; }
       
    } 
}
