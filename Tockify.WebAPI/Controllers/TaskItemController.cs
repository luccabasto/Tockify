using Microsoft.AspNetCore.Mvc;
using Tockify.Application.DTOs;
using Tockify.Application.Services.UseCases.Interfaces;

namespace Tockify.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ICreateTaskItemUseCase _createUseCase;
        private readonly IGetTaskItemsByTaskListUseCase _getByTaskUseCase;

        public TaskItemController(
            ICreateTaskItemUseCase createUseCase,
            IGetTaskItemsByTaskListUseCase getByTaskUseCase)
        {
            _createUseCase = createUseCase;
            _getByTaskUseCase = getByTaskUseCase;
        }

        [HttpGet("task/{taskListId:guid}")]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetByTaskId(Guid taskListId)
        {
            var dtos = await _getByTaskUseCase.ExecuteAsync(taskListId);
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> Create([FromBody] CreateTaskItemCommand command)
        {
            try
            {
                var createdDto = await _createUseCase.ExecuteAsync(command);
                return CreatedAtAction(nameof(GetByTaskId), new { taskListId = createdDto.TaskListId }, createdDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
