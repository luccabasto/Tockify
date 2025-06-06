using Microsoft.EntityFrameworkCore;
using Tockify.Domain.Enums;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;
using Tockify.Infrastructure.Data;

namespace Tockify.Domain.Repository
{
    public class ClientRepository : IClientUserRepository
    {
        private readonly TockifyDBContext _dbContext;
        public ClientRepository(TockifyDBContext tockifyDBContext)
        {
            _dbContext = tockifyDBContext;
        }

        // Métodos para gerenciar usuários do cliente
        public async Task<List<ClientUserModel>> GetAllClientUsers(UserProfile userProfile)
        {
            return await _dbContext.ClientUsers.ToListAsync();
        }

        public async Task<ClientUserModel?> GetUserByIdAsync(Guid id)
        {
            return await _dbContext.ClientUsers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ClientUserModel?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.ClientUsers
                .FirstOrDefaultAsync(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        // CRUD operações para usuários do cliente 
        public async Task<ClientUserModel> RegisterUserAsync( ClientUserModel clientUser)
        {
            await _dbContext.ClientUsers.AddAsync(clientUser);
            await _dbContext.SaveChangesAsync();

            return clientUser;
        }

        public async Task<ClientUserModel> UpdateClientUserByIdAsync(ClientUserModel user, Guid id)
        {
            // Recebendo o ID do usuário a ser atualizado
            ClientUserModel? clientById = await GetUserByIdAsync(id);

            // Check se o usuário existe
            if (clientById == null)
            {
                throw new Exception($"Usuário para o ID:{id} não foi encontrado no banco de dados.");
            }

            // Update propriedades do usuário
            clientById.Name = user.Name;
            clientById.Email = user.Email;
            clientById.Password = user.Password;
            clientById.Gender = user?.Gender;
            clientById.IsActive = user.IsActive;

            // Salvando as alterações no banco de dados
            _dbContext.ClientUsers.Update(clientById);
            await _dbContext.SaveChangesAsync();

            // Me retorna o usuário atualizado
            return clientById;
        }

        public async Task<string> DeleteClientUserByIdAsync(ClientUserModel user, Guid id)
        {
            ClientUserModel? clientById = await GetUserByIdAsync(id);

            if (clientById == null)
            {
                return $"Usuário para o ID:{id} não foi encontrado no banco de dados.";
            }

            _dbContext.ClientUsers.Remove(clientById);
            await _dbContext.SaveChangesAsync();

            return "Usuário excluído com sucesso.";
        }

        // Métodos adicionais para gerenciamento de usuários 
        public async Task<string> ClientUserExistsAsync(ClientUserModel user, Guid id)
        {
            ClientUserModel? clientUserModel = await GetUserByIdAsync(id);

            if (clientUserModel == null)
            {
                return $"Usuário {clientUserModel.Name} não se encontra ativo.";
            }

            return $"Usuário {clientUserModel.Name} está ativo e registrado com sucesso.";

        }
    }
}
