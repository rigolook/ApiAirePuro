using Airepuro.Api.Models;
using Airepuro.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Airepuro.Api.Services
{
    public class VmonitoreoServices
    {
        private readonly IMongoCollection<Vmonitoreo> _VmonitoreoCollection;
        private readonly IMongoCollection<SensorAire> _sensorAireCollection;
        private readonly IMongoCollection<SensorTemperatura> _sensorTemperaturaCollection;
        private readonly IMongoCollection<Ventilador> _ventiladorCollection;

        public VmonitoreoServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _VmonitoreoCollection =
                mongoDB.GetCollection<Vmonitoreo>(databaseSettings.Value.Collections["Vmonitoreo"]);
            _sensorAireCollection =
                mongoDB.GetCollection<SensorAire>(databaseSettings.Value.Collections["SensorAire"]);
            _sensorTemperaturaCollection =
                mongoDB.GetCollection<SensorTemperatura>(databaseSettings.Value.Collections["SensorTemperatura"]);
            _ventiladorCollection =
                mongoDB.GetCollection<Ventilador>(databaseSettings.Value.Collections["Ventilador"]);
        }

        public async Task<List<Vmonitoreo>> GetAsync()
        {
            try
            {
                var vmonitoreos = await _VmonitoreoCollection.Find(_ => true).ToListAsync();
                foreach (var vmonitoreo in vmonitoreos)
                {
                    var sensoraires = await _sensorAireCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(vmonitoreo.SensorAireId) } }).Result.ToListAsync();
                    var sensortemperaturas = await _sensorTemperaturaCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(vmonitoreo.SensorTemperaturaId) } }).Result.ToListAsync();
                    var ventiladors = await _ventiladorCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(vmonitoreo.VentiladorId) } }).Result.ToListAsync();
                    if (sensoraires.Any() && sensortemperaturas.Any() && ventiladors.Any())
                    {
                        var sensoraire = sensoraires.First();
                        var sensortemperatura = sensortemperaturas.First();
                        var ventilador = ventiladors.First();
                        vmonitoreo.SensorAire = sensoraire;
                        vmonitoreo.SensorTemperatura = sensortemperatura;
                        vmonitoreo.Ventilador = ventilador;
                    }
                }
                return vmonitoreos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos de la base de datos", ex);
            }
        }

        public async Task InsertVmonitoreo(Vmonitoreo VmonitoreoInsert)
        {
            await _VmonitoreoCollection.InsertOneAsync(VmonitoreoInsert);
        }

        public async Task DeleteVmonitoreo(string VmonitoreoId)
        {
            var filter = Builders<Vmonitoreo>.Filter.Eq(s => s.Id, VmonitoreoId);
            await _VmonitoreoCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateVmonitoreo(Vmonitoreo dataToUpdate)
        {
            var filter = Builders<Vmonitoreo>.Filter.Eq(s => s.Id, dataToUpdate.Id);
            await _VmonitoreoCollection.ReplaceOneAsync(filter, dataToUpdate);
        }

        public async Task<Vmonitoreo> GetVmonitoreoById(string idToSearch)
        {
            return await _VmonitoreoCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(idToSearch) } }).Result.FirstAsync();
        }
    }
}
