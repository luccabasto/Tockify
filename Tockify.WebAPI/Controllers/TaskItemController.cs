using Microsoft.AspNetCore.Mvc;
using Tockify.Application.Command.TaskItem;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.TaskItem;

namespace Tockify.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ICreateTaskItemCase _createCase;
        private readonly IGetToDoTasksCase _getCase;
        private readonly IUpdateTaskItemCase _updateCase;
        private readonly IDeleteTaskItemCase _deleteCase;

        public TaskItemController(
            ICreateTaskItemCase createCase,
            IGetToDoTasksCase getCase,
            IUpdateTaskItemCase updateCase,
            IDeleteTaskItemCase deleteCase)
        {
            _createCase = createCase;
            _getCase = getCase;
            _updateCase = updateCase;
            _deleteCase = deleteCase;
        }

        /// <summary>
        /// Criar um novo item de tarefa.
        /// </summary>
        /// <returns>O item de tarefa criado.</returns>
        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> Post([FromBody] CreateTaskItemCommand cmd)
        {
            var dto = await _createCase.CreateTaskItemAsync(cmd);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id, userId = dto.CreatedByUserId }, dto);
        }

        /// <summary>
        ///  Pegar todos os itens de tarefa de um ToDo específico.
        ///  </summary>
        ///  <returns>Uma lista de itens de tarefa.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> Get([FromQuery] string toDoId, [FromQuery] int userId)
        {
            var list = await _getCase.GetToDoTasksAsync(toDoId, userId);
            return Ok(list);
        }

        /// <summary>
        /// Retorna um item de tarefa específico pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetById(string id, [FromQuery] int userId)
        {
            var items = await _getCase.GetToDoTasksAsync("", userId); // Corrigido para usar GetToDoTasksAsync
            var item = items.Find(t => t.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Atualiza um item de tarefa existente.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>O item de tarefa atualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItemDto>> Put(string id, [FromBody] UpdateTaskItemCommand cmd)
        {
            cmd.Id = id;
            var dto = await _updateCase.UpdateTaskItemAsync(cmd);
            return Ok(dto);
        }

        /// <summary>
        /// Deleta um item de tarefa existente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromQuery] int userId)
        {
            var ok = await _deleteCase.DeleteTaskItemAsync(id, userId);
            return ok ? NoContent() : NotFound();
        }
    }
}
