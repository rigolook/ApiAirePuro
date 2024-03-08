using Airepuro.Api.Models;
using Airepuro.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Airepuro.Api.Services
{
    public class VentiladorServices
    {
        private readonly IMongoCollection<Ventilador> _ventiladorCollection;

        public VentiladorServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _ventiladorCollection =
                mongoDB.GetCollection<Ventilador>(databaseSettings.Value.Collections["Ventilador"]);
        }

        public async Task<List<Ventilador>> GetAsync() =>
        await _ventiladorCollection.Find(_ => true).ToListAsync();

        public async Task InsertVentilador(Ventilador VentiladorInsert)
        {
            await _ventiladorCollection.InsertOneAsync(VentiladorInsert);
        }

        public async Task DeleteVentilador(string VentiladorId)
        {
            var filter = Builders<Ventilador>.Filter.Eq(s => s.Id, VentiladorId);
            await _ventiladorCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateVentilador(Ventilador dataToUpdate)
        {
            var filter = Builders<Ventilador>.Filter.Eq(s => s.Id, dataToUpdate.Id);
            await _ventiladorCollection.ReplaceOneAsync(filter, dataToUpdate);
        }

        public async Task<Ventilador> GetVentiladorById(string idToSearch)
        {
            return await _ventiladorCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(idToSearch) } }).Result.FirstAsync();
        }
    }
}
