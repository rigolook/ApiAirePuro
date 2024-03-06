using Airepuro.Api.Models;
using Airepuro.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Airepuro.Api.Services
{
    public class SensorTemperaturaService
    {
        private readonly IMongoCollection<SensorTemperatura> _SensorTemperaturaCollection;

        public SensorTemperaturaService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _SensorTemperaturaCollection =
                mongoDB.GetCollection<SensorTemperatura>(databaseSettings.Value.Collections["SensorTemperatura"]);
        }

        public async Task<List<SensorTemperatura>> GetAsync() =>
        await _SensorTemperaturaCollection.Find(_ => true).ToListAsync();

        public async Task InsertSensorTemperatura(SensorTemperatura SensorTemperaturaInsert)
        {
            await _SensorTemperaturaCollection.InsertOneAsync(SensorTemperaturaInsert);
        }

        public async Task DeleteSensorTemperatura(string SensorTemperaturaId)
        {
            var filter = Builders<SensorTemperatura>.Filter.Eq(s => s.Id, SensorTemperaturaId);
            await _SensorTemperaturaCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateSensorTemperatura(SensorTemperatura dataToUpdate)
        {
            var filter = Builders<SensorTemperatura>.Filter.Eq(s => s.Id, dataToUpdate.Id);
            await _SensorTemperaturaCollection.ReplaceOneAsync(filter, dataToUpdate);
        }

        public async Task<SensorTemperatura> GetSensorTemperaturaById(string idToSearch)
        {
            return await _SensorTemperaturaCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(idToSearch) } }).Result.FirstAsync();
        }
    }
}
