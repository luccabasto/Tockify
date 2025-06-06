using Microsoft.AspNetCore.Mvc;
using Tockify.Application.DTOs;
using Tockify.Application.Services.UseCases.Interfaces;

namespace Tockify.WebAPI.Controllers
{

    /// <summary>
    /// Controller para gerenciar usuários do cliente.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientUserController : ControllerBase
    {
        private readonly ICreateClientUserUseCase _createUseCase;
        private readonly IGetAllClientUsersUseCase _getAllUseCase;

        public ClientUserController(
            ICreateClientUserUseCase createUseCase,
            IGetAllClientUsersUseCase getAllUseCase)
        {
            _createUseCase = createUseCase;
            _getAllUseCase = getAllUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientUserDto>>> GetAll()
        {
            var dtos = await _getAllUseCase.ExecuteAsync();
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<ClientUserDto>> Create([FromBody] CreateClientUserCommand command)
        {
            try
            {
                var createdDto = await _createUseCase.ExecuteAsync(command);
                return CreatedAtAction(nameof(GetAll), null, createdDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
