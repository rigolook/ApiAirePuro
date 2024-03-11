using Airepuro.Api.Models;
using Airepuro.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Airepuro.Api.Services
{
    public class VloginServices
    {
        private readonly IMongoCollection<Vlogin> _vloginCollection;
        private readonly IMongoCollection<Usuario> _usuarioCollection;

        public VloginServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _vloginCollection =
                mongoDB.GetCollection<Vlogin>(databaseSettings.Value.Collections["Vlogin"]);
            _usuarioCollection =
                mongoDB.GetCollection<Usuario>(databaseSettings.Value.Collections["Usuario"]);
        }

        public async Task<List<Vlogin>> GetAsync() =>
        await _vloginCollection.Find(_ => true).ToListAsync();
        public async Task InsertVlogin(Vlogin VloginInsert)
        {
            await _vloginCollection.InsertOneAsync(VloginInsert);
        }

        public async Task DeleteVlogin(string VloginId)
        {
            var filter = Builders<Vlogin>.Filter.Eq(s => s.Id, VloginId);
            await _vloginCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateVlogin(Vlogin dataToUpdate)
        {
            var filter = Builders<Vlogin>.Filter.Eq(s => s.Id, dataToUpdate.Id);
            await _vloginCollection.ReplaceOneAsync(filter, dataToUpdate);
        }

        public async Task<Vlogin> GetVloginById(string idToSearch)
        {
            return await _vloginCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(idToSearch) } }).Result.FirstAsync();
        }
    }
}
