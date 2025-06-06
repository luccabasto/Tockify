using MongoDB.Driver;
using Tockify.Domain.Enums;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;
using Tockify.Infrastructure.Data;

namespace Tockify.Infrastructure.Repositories
{
    public class ClientRepository : IClientUserRepository
    {
        private readonly MongoContext _context;

        public ClientRepository(MongoContext mongoContext)
        {
            _context = mongoContext;
        }

        // Retorna todos usuários cujo Profile == userProfile
        public async Task<List<ClientUserModel>> GetAllClientUsers(UserProfile userProfile)
        {
            var filter = Builders<ClientUserModel>.Filter.Eq(x => x.Profile, userProfile);
            return await _context.ClientUsers.Find(filter).ToListAsync();
        }

        // Retorna usuário por Guid Id
        public async Task<ClientUserModel?> GetUserByIdAsync(Guid id)
        {
            var filter = Builders<ClientUserModel>.Filter.Eq(u => u.Id, id);
            return await _context.ClientUsers.Find(filter).FirstOrDefaultAsync();
        }

        // Retorna usuário por email (ignorando maiúsculas/minúsculas)
        public async Task<ClientUserModel?> GetUserByEmailAsync(string email)
        {
            var filter = Builders<ClientUserModel>.Filter.Eq(u => u.Email, email.ToLower().Trim());
            return await _context.ClientUsers.Find(filter).FirstOrDefaultAsync();
        }

        // Cria um novo usuário
        public async Task<ClientUserModel> RegisterUserAsync(ClientUserModel user, string email, string password)
        {
            user.Email = email.ToLower().Trim();
            if (user.Id == Guid.Empty)
                user.Id = Guid.NewGuid();

            user.Password = password;
            user.Profile = UserProfile.Client;
            user.IsActive = true;
            user.CreatedAt = DateTime.UtcNow;

            await _context.ClientUsers.InsertOneAsync(user);
            return user;
        }

        // Atualiza dados do usuário existente (Name, Email, Password, IsActive)
        public async Task<ClientUserModel> UpdateClientUserByIdAsync(ClientUserModel user, string email, string password)
        {
            var filter = Builders<ClientUserModel>.Filter.Eq(u => u.Id, user.Id);
            var update = Builders<ClientUserModel>.Update
                .Set(u => u.Name, user.Name.Trim())
                .Set(u => u.Email, email.ToLower().Trim())
                .Set(u => u.Password, password)
                .Set(u => u.IsActive, user.IsActive);

            await _context.ClientUsers.UpdateOneAsync(filter, update);
            return user;
        }

        // Exclui usuário pelo Id
        public async Task<bool> DeleteClientUserByIdAsync(Guid id)
        {
            var filter = Builders<ClientUserModel>.Filter.Eq(u => u.Id, id);
            var result = await _context.ClientUsers.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        // Verifica existência de usuário por email (retorna true/false)
        public async Task<bool> ClientUserExistsAsync(string email)
        {
            var filter = Builders<ClientUserModel>.Filter.Eq(u => u.Email, email.ToLower().Trim());
            return await _context.ClientUsers.Find(filter).AnyAsync();
        }
    }
}
