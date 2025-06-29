using Microsoft.AspNetCore.Mvc;
using Tockify.Application.Command.ToDo;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ToDo;

namespace Tockify.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ICreateToDoCase _createCase;
        private readonly IGetUserToDosCase _getCase;
        private readonly IUpdateToDoCase _updateCase;
        private readonly IDeleteToDoCase _deleteCase;

        public ToDoController(
            ICreateToDoCase createCase,
            IGetUserToDosCase getCase,
            IUpdateToDoCase updateCase,
            IDeleteToDoCase deleteCase)
        {
            _createCase = createCase;
            _getCase = getCase;
            _updateCase = updateCase;
            _deleteCase = deleteCase;
        }
        /// <summary>
        /// Cria um novo ToDo.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ToDoDto>> Post([FromBody] CreateToDoCommand command)
        {
            var dto = await _createCase.CreateToDoAsync(command);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Retorna uma lista de ToDos para um usuário específico.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoDto>>> Get([FromQuery] int userId)
        {
            var list = await _getCase.ExecuteAsync(userId);
            return Ok(list);
        }

        /// <summary>
        /// Retorna um ToDo específico pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoDto>> GetById(string id, [FromQuery] int userId)
        {
            var todos = await _getCase.ExecuteAsync(userId);
            var item = todos.Find(t => t.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Atualiza um ToDo existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoDto>> Put(string id, [FromBody] UpdateToDoCommand command)
        {
            command.id = id;
            var dto = await _updateCase.UpdateToDoAsync(command);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromQuery] int userId)
        {
            var ok = await _deleteCase.DeleteToDoAsync(id, userId);
            return ok ? NoContent() : NotFound();
        }
    }
}
