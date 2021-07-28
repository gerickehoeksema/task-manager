using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Application.Member.Command;
using TaskManager.Application.Member.Query;

namespace TaskManager.API.Controllers
{
    /// <summary>
    /// Member controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ApiBaseController
    {
        internal ILogger<MemberController> logger;

        /// <summary>
        /// Member Controller Constructor
        /// </summary>
        /// <param name="logger"></param>
        public MemberController(ILogger<MemberController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Get list of active members
        /// </summary>
        [HttpGet("list", Order = 1)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<MemberDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await Mediator
                    .Send(new GetMemberListQuery())
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
        /// Get a member
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}", Order = 2)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MemberDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var result = await Mediator
                    .Send(new GetMemberByIdQuery { MemberId = id})
                    .ConfigureAwait(false);

                return result != null ? Ok(result) : NotFound($"Unable to find member with id: {id}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a member
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}", Order = 3)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await Mediator
                    .Send(new DeleteMemberCommand
                    {
                        MemberId = id
                    })
                    .ConfigureAwait(false);

                return result ? NoContent() : BadRequest("Unable to delete member");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
