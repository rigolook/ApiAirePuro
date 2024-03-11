using Airepuro.Api.Models;
using Airepuro.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Airepuro.Api.Services
{
    public class VhistorialServices
    {
        private readonly IMongoCollection<Vhistorial> _VhistorialCollection;
        private readonly IMongoCollection<SensorAire> _sensorAireCollection;
        private readonly IMongoCollection<SensorTemperatura> _sensorTemperaturaCollection;
        private readonly IMongoCollection<Ventilador> _ventiladorCollection;

        public VhistorialServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _VhistorialCollection =
                mongoDB.GetCollection<Vhistorial>(databaseSettings.Value.Collections["Vhistorial"]);
            _sensorAireCollection =
                mongoDB.GetCollection<SensorAire>(databaseSettings.Value.Collections["SensorAire"]);
            _sensorTemperaturaCollection =
                mongoDB.GetCollection<SensorTemperatura>(databaseSettings.Value.Collections["SensorTemperatura"]);
            _ventiladorCollection =
                mongoDB.GetCollection<Ventilador>(databaseSettings.Value.Collections["Ventilador"]);
        }

        public async Task<List<Vhistorial>> GetAsync()
        {
            try
            {
                var vhistorials = await _VhistorialCollection.Find(_ => true).ToListAsync();
                foreach (var vhistorial  in vhistorials)
                {
                    var sensoraires = await _sensorAireCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(vhistorial.SensorAireId) } }).Result.ToListAsync();
                    var sensortemperaturas = await _sensorTemperaturaCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(vhistorial.SensorTemperaturaId) } }).Result.ToListAsync();
                    var ventiladors = await _ventiladorCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(vhistorial.VentiladorId) } }).Result.ToListAsync();
                    if (sensoraires.Any() && sensortemperaturas.Any() && ventiladors.Any())
                    {
                        var sensoraire = sensoraires.First();
                        var sensortemperatura = sensortemperaturas.First();
                        var ventilador = ventiladors.First();
                        vhistorial.SensorAire = sensoraire;
                        vhistorial.SensorTemperatura = sensortemperatura;
                        vhistorial.Ventilador = ventilador;
                    }
                }
                return vhistorials;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos de la base de datos", ex);
            }
        }

        public async Task InsertVhistorial(Vhistorial VhistorialInsert)
        {
            await _VhistorialCollection.InsertOneAsync(VhistorialInsert);
        }

        public async Task DeleteVhistorial(string VhistorialId)
        {
            var filter = Builders<Vhistorial>.Filter.Eq(s => s.Id, VhistorialId);
            await _VhistorialCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateVhistorial(Vhistorial dataToUpdate)
        {
            var filter = Builders<Vhistorial>.Filter.Eq(s => s.Id, dataToUpdate.Id);
            await _VhistorialCollection.ReplaceOneAsync(filter, dataToUpdate);
        }

        public async Task<Vhistorial> GetVhistorialById(string idToSearch)
        {
            return await _VhistorialCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(idToSearch) } }).Result.FirstAsync();
        }
    }
        
}
