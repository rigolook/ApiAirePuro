using Airepuro.Api.Models;
using Airepuro.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Airepuro.Api.Services
{
    public class VregistrarCuentaServices
    {
        private readonly IMongoCollection<VregistrarCuenta> _VregistrarCuentaCollection;
        private readonly IMongoCollection<Usuario> _UsuarioCollection;
        

        public VregistrarCuentaServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _VregistrarCuentaCollection =
                mongoDB.GetCollection<VregistrarCuenta>(databaseSettings.Value.Collections["VregistrarCuenta"]);
            _UsuarioCollection =
                mongoDB.GetCollection<Usuario>(databaseSettings.Value.Collections["Usuario"]);
           
        }

        public async Task<List<VregistrarCuenta>> GetAsync()
        {
            try
            {
                var vregistrars = await _VregistrarCuentaCollection.Find(_ => true).ToListAsync();
                foreach (var vregistrar in vregistrars)
                {
                    var usuarios = await _UsuarioCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(vregistrar.UsuarioId) } }).Result.ToListAsync();
                    if (usuarios.Any())
                    {
                        var usuario = usuarios.First();
                        vregistrar.Usuario = usuario;
                       
                    }
                }
                return vregistrars;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos de la base de datos", ex);
            }
        }

        public async Task InsertVregistrarCuenta(VregistrarCuenta VregistrarCuentaInsert)
        {
            await _VregistrarCuentaCollection.InsertOneAsync(VregistrarCuentaInsert);
        }

        public async Task DeleteVregistrarCuenta(string VregistrarCuentaId)
        {
            var filter = Builders<VregistrarCuenta>.Filter.Eq(s => s.Id, VregistrarCuentaId);
            await _VregistrarCuentaCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateVregistrarCuenta(VregistrarCuenta dataToUpdate)
        {
            var filter = Builders<VregistrarCuenta>.Filter.Eq(s => s.Id, dataToUpdate.Id);
            await _VregistrarCuentaCollection.ReplaceOneAsync(filter, dataToUpdate);
        }

        public async Task<VregistrarCuenta> GetVregistrarCuentaById(string idToSearch)
        {
            return await _VregistrarCuentaCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(idToSearch) } }).Result.FirstAsync();
        }
    }
}
