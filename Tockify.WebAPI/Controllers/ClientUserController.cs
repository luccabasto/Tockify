using Microsoft.AspNetCore.Mvc;
using Tockify.Application.Command.ClientUser;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ClientUser;
using Tockify.Domain.Enums;

namespace Tockify.WebAPI.Controllers
{
    /// <summary>
    /// Controller para gerenciar usuários do cliente.
    /// </summary>
    /// <returns> Operações com o Client User</returns>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientUserController : ControllerBase
    {
        private readonly ICreateClientUserCase _createUseCase;
        private readonly IGetAllClientUsersCase _getAllUseCase;
        private readonly IGetClientUserByIdCase _getByIdUseCase;
        private readonly IUpdateClientUseCase _updateUseCase;
        private readonly IDeleteClientUserCase _deleteUseCase;

        public ClientUserController(
            ICreateClientUserCase createUseCase,
            IGetAllClientUsersCase getAllUseCase,
            IGetClientUserByIdCase getByIdUseCase,
            IUpdateClientUseCase updateUseCase,
            IDeleteClientUserCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
        }

        /// <summary>
        /// Retorne todos os usuários do tipo Client.
        /// </summary>
        /// <returns>Uma lista de usuários do tipo Client.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientUserDto>>> GetAll(
            [FromQuery] UserProfile? profile,
            [FromQuery] bool? isActive,
            [FromQuery] string? name)
        {
            var dtos = await _getAllUseCase.GetAllClient(profile, isActive, name);
            return Ok(dtos);
        }

        /// <summary>
        /// Retorna um usuário do tipo Client pelo ID.
        /// </summary>
        /// <returns>O usuário encontrado ou uma mensagem de erro.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientUserDto>> GetById(int id)
        {
            try
            {
                var dtos = await _getByIdUseCase.GetUserByIdAsync(id);
                if (dtos == null)
                {
                    return NotFound(new { message = $"Usuário com ID {id} não encontrado." });
                }
                return Ok(new { message = "Usuário encontrado com sucesso.", data = dtos });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Criando um novo usuário do tipo Client.
        /// </summary>
        /// <returns>O usuário criado ou uma mensagem de erro.</returns>
        [HttpPost]
        public async Task<ActionResult<ClientUserDto>> CreateClient([FromBody] CreateClientUserCommand command)
        {
            try
            {
                var createdDto = await _createUseCase.CreateClientUser(command);
                return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao criar usuário do tipo Client.", error = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um usuário do tipo Client pelo ID.
        /// </summary>
        /// <returns>O usuário atualizado ou uma mensagem de erro.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ClientUserDto>> UpdateClient(int id, [FromBody] UpdateClientUserCommand command)
        {
            try
            {
                if (id != command.Id)
                {
                    return BadRequest(new { message = "ID do usuário não corresponde ao ID do comando." });
                }
                var updatedDto = await _updateUseCase.UpdateClientUser(command);
                return Ok(updatedDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao atualizar usuário do tipo Client.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            try
            {
                await _deleteUseCase.DeleteClientUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao excluir usuário do tipo Client.", error = ex.Message });
            }
        }
    }
}