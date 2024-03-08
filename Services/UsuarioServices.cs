using Airepuro.Api.Models;
using Airepuro.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Airepuro.Api.Services
{
    public class UsuarioServices
    {
        private readonly IMongoCollection<Usuario> _UsuarioCollection;

        public UsuarioServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _UsuarioCollection =
                mongoDB.GetCollection<Usuario>(databaseSettings.Value.Collections["Usuario"]);
        }

        public async Task<List<Usuario>> GetAsync() =>
        await _UsuarioCollection.Find(_ => true).ToListAsync();

        public async Task InsertUsuario(Usuario UsuarioInsert)
        {
            await _UsuarioCollection.InsertOneAsync(UsuarioInsert);
        }

        public async Task DeleteUsuario(string UsuarioId)
        {
            var filter = Builders<Usuario>.Filter.Eq(s => s.Id, UsuarioId);
            await _UsuarioCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateUsuario(Usuario dataToUpdate)
        {
            var filter = Builders<Usuario>.Filter.Eq(s => s.Id, dataToUpdate.Id);
            await _UsuarioCollection.ReplaceOneAsync(filter, dataToUpdate);
        }

        public async Task<Usuario> GetUsuarioById(string idToSearch)
        {
            return await _UsuarioCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(idToSearch) } }).Result.FirstAsync();
        }
    }
}
