using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Airepuro.Api.Models;

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
        
        [BsonElement("SensorAireId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SensorAireId { get; set; } = string.Empty;



        [BsonElement("SensorTemperaturaId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SensorTemperaturaId { get; set; } = string.Empty;


        [BsonElement("VentiladorId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string VentiladorId { get; set; } = string.Empty;

        [BsonIgnore]
        public SensorAire? SensorAire { get; set; }
        [BsonIgnore]
        public SensorTemperatura? SensorTemperatura { get;set; }
        [BsonIgnore]
        public Ventilador? Ventilador { get;set; }
    }
}    
    

