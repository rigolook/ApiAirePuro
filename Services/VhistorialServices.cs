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

        public VhistorialServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _VhistorialCollection =
                mongoDB.GetCollection<Vhistorial>(databaseSettings.Value.Collections["Vhistorial"]);
        }

        public async Task<List<Vhistorial>> GetAsync() =>
        await _VhistorialCollection.Find(_ => true).ToListAsync();

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
