using Airepuro.Api.Models;
using Airepuro.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Airepuro.Api.Services
{
    public class SensorAireServices
    {
        private readonly IMongoCollection<SensorAire> _sensorAireCollection;

        public SensorAireServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _sensorAireCollection =
                mongoDB.GetCollection<SensorAire>(databaseSettings.Value.Collections["SensorAire"]);
        }

        public async Task<List<SensorAire>> GetAsync() =>
        await _sensorAireCollection.Find(_ => true).ToListAsync();

        public async Task InsertSensorAire(SensorAire sensorAireInsert)
        {
            await _sensorAireCollection.InsertOneAsync(sensorAireInsert);
        }

        public async Task DeleteSensorAire(string SensorAireId)
        {
            var filter = Builders<SensorAire>.Filter.Eq(s => s.Id, SensorAireId);
            await _sensorAireCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateSensorAire(SensorAire dataToUpdate)
        {
            var filter = Builders<SensorAire>.Filter.Eq(s => s.Id, dataToUpdate.Id);
            await _sensorAireCollection.ReplaceOneAsync(filter, dataToUpdate);
        }

        public async Task<SensorAire> GetSensorAireById(string idToSearch)
        {
            return await _sensorAireCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(idToSearch) } }).Result.FirstAsync();
        }
    }
}
