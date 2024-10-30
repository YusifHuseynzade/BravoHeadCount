using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectDetails.Queries.Request;

namespace FinanceOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetProjectByEmail")]
        public async Task<IActionResult> GetProjectByEmail([FromQuery] GetProjectByEmailRequest request)
        {
            var response = await _mediator.Send(request);
            if (!response.IsSuccess)
                return BadRequest(response.Message);

            return Ok(response);
        }
    }

}

