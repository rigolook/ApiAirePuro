using Airepuro.Api.Models;
using Airepuro.Configurations;
using Microsoft.Extensions.Options;
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
                mongoDB.GetCollection<SensorAire>(databaseSettings.Value.CollectionName);
        }

        public async Task<List<SensorAire>> GetAsync() =>
        await _sensorAireCollection.Find(_ => true).ToListAsync();
    }
}
