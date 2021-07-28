using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Application.Tasks.Command;
using TaskManager.Application.Tasks.Query;

namespace TaskManager.API.Controllers
{
    /// <summary>
    /// Task API Controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ApiBaseController
    {
        internal ILogger<TaskController> logger;

        /// <summary>
        /// Task API Controller Constructor
        /// </summary>
        /// <param name="logger"></param>
        public TaskController(ILogger<TaskController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Get list of active tasks
        /// </summary>
        [HttpGet("list", Order = 1)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TaskDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await Mediator.
                    Send(new GetTaskListQuery())
                    .ConfigureAwait(false);

                return result.Count > 0 ? Ok(result) : NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a task
        /// </summary>
        [HttpGet("{id}", Order = 2)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var result = await Mediator
                    .Send(new GetTaskQuery { TaskId = id })
                    .ConfigureAwait(false);

                return result != null ? Ok(result) : NotFound($"Unable to find task with id: {id}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Create a new task
        /// </summary>
        [HttpPost(Order = 3)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] TaskDTO task)
        {
            try
            {
                var result = await Mediator
                    .Send(new CreateTaskCommand { 
                        Title = task.Title,
                        Description = task.Description,
                        Status = task.Status
                    })
                    .ConfigureAwait(false);

                return result ? StatusCode(StatusCodes.Status201Created) : BadRequest("Unable to create task");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update a new task
        /// </summary>
        [HttpPut(Order = 4)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] TaskDTO task)
        {
            try
            {
                var result = await Mediator
                    .Send(new UpdateTaskCommand
                    {
                        TaskId = task.Id,
                        Title = task.Title,
                        Description = task.Description,
                        Status = task.Status,
                        StartTime = task.StartTime,
                        EndDate = task.EndDate,
                        AssignedTo = task.AssignedTo
                    })
                    .ConfigureAwait(false);

                return result ? Ok(task) : BadRequest("Unable to update task");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update a new task
        /// </summary>
        [HttpPut("assign/{taskId}/{memberId}",Order = 5)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Assign(long taskId, long memberId)
        {
            try
            {
                var result = await Mediator
                    .Send(new AssignTaskCommand
                    {
                        TaskId = taskId,
                        MemberId = memberId
                    })
                    .ConfigureAwait(false);

                return result ? NoContent() : BadRequest("Unable to assign task");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a new task
        /// </summary>
        [HttpDelete("{id}",Order = 6)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await Mediator
                    .Send(new DeleteTaskCommand
                    {
                        TaskId = id
                    })
                    .ConfigureAwait(false);

                return result ? NoContent() : BadRequest("Unable to update task");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
