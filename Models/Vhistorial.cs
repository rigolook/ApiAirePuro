using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Airepuro.Api.Models
{
    public class Vhistorial
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Titulo")]
        public string Titulo { get; set; } = string.Empty;
        [BsonElement("Fecha")]
        public string Fecha { get; set; } = string.Empty;
        [BsonElement("Hora")]
        public string Hora { get; set; } = string.Empty;
        [BsonElement("SensorAire")]
        public object SensorAire { get; set; }
        [BsonElement("SensorTemperatura")]
        public object SensorTemperatura { get; set; }
        [BsonElement("Ventilador")]
        public object Ventilador { get; set; }


    }
}
