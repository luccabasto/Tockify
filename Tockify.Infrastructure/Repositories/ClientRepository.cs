using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tockify.Domain.Enums;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;
using Tockify.Infrastructure.Context;

namespace Tockify.Infrastructure.Repositories
{
    public class ClientUserRepository : IClientUserRepository
    {
        private readonly IMongoCollection<ClientUserModel> _collection;
        private readonly IMongoCollection<CounterModel> _counterCollection;

        public ClientUserRepository(MongoContext context)
        {
            _collection = context.Database.GetCollection<ClientUserModel>("client_users");
            _counterCollection = context.Database.GetCollection<CounterModel>("counters");
        }

        private int GetNextSequenceValue(string name)
        {
            var filter = Builders<CounterModel>.Filter.Eq(c => c.Id, name);
            var update = Builders<CounterModel>.Update.Inc(c => c.SequenceValue, 1);
            var options = new FindOneAndUpdateOptions<CounterModel>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = true
            };

            var result = _counterCollection.FindOneAndUpdate(filter, update, options);
            return result.SequenceValue;
        }

        public async Task<List<ClientUserModel>> GetAllClientUsersAsync(UserProfile profile)
        {
            return await _collection
                         .Find(u => u.Profile == profile)
                         .ToListAsync();
        }

        public async Task<ClientUserModel?> GetUserByIdAsync(int id)
            => await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task<ClientUserModel?> GetUserByEmailAsync(string email)
            => await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task<ClientUserModel> RegisterUserAsync(ClientUserModel user, string email, string password)
        {
            user.Id = GetNextSequenceValue("client_user_id");
            user.Email = email;
            user.Password = password;
            await _collection.InsertOneAsync(user);
            return user;
        }

        public async Task<ClientUserModel> UpdateClientUserByIdAsync(ClientUserModel user, string email, string password)
        {
            user.Email = email;
            user.Password = password;
            await _collection.ReplaceOneAsync(u => u.Id == user.Id, user);
            return user;
        }

        public async Task<ClientUserModel> DeleteClientUserByIdAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
                await _collection.DeleteOneAsync(u => u.Id == id);
            return user!;
        }

        public async Task<bool> ClientUserExistsAsync(string email)
            => await _collection.Find(u => u.Email == email).AnyAsync();

        public async Task IncrementIncompleteCountAsync(int userId)
        {
            var filter = Builders<ClientUserModel>.Filter.Eq(u => u.Id, userId);
            var update = Builders<ClientUserModel>.Update.Inc(u => u.IncompleteToDosCount, 1);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task DecrementIncompleteCountAsync(int userId)
        {
            var filter = Builders<ClientUserModel>.Filter.Eq(u => u.Id, userId);
            var update = Builders<ClientUserModel>.Update.Inc(u => u.IncompleteToDosCount, -1);
            await _collection.UpdateOneAsync(filter, update);
        }
    }
}