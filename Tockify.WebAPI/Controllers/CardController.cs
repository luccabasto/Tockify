using Microsoft.AspNetCore.Mvc;
using Tockify.Application.Command.ToDo;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ToDo;

namespace Tockify.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICreateToDotUseCase _createUseCase;
        private readonly IGetToDoByUserUseCase _getByUserUseCase;

        public CardController(
            ICreateToDotUseCase createUseCase,
            IGetToDoByUserUseCase getByUserUseCase)
        {
            _createUseCase = createUseCase;
            _getByUserUseCase = getByUserUseCase;
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<IEnumerable<ToDoDto>>> GetByUserId(Guid userId)
        {
            var dtos = await _getByUserUseCase.ExecuteAsync(userId);
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<ToDoDto>> Create([FromBody] CreateToDoCommand command)
        {
            try
            {
                await _createUseCase.ExecuteAsync(command);
                return CreatedAtAction(nameof(GetByUserId), new { userId = command.UserId }, command);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
