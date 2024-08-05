using FunctionalAreaDetails.Commands.Request;
using FunctionalAreaDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class FunctionalAreaController : ControllerBase
	{
		private readonly IMediator _mediator;
		public FunctionalAreaController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateFunctionalAreaCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteFunctionalAreaCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] GetAllFunctionalAreaQueryRequest request)
		{
			var districts = await _mediator.Send(request);

			return Ok(districts);
		}
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateFunctionalAreaCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var requestModel = new GetByIdFunctionalAreaQueryRequest { Id = id };
			var funtionalArea = await _mediator.Send(requestModel);

			return funtionalArea != null
				? (IActionResult)Ok(funtionalArea)
				: NotFound(new { Message = "FunctionalArea not found." });
		}

        [HttpGet("GetFunctionalAreaProjects")]
        public async Task<IActionResult> GetFunctionalAreaProjects([FromQuery] GetFunctionalAreaProjectsQueryRequest request)
        {
            var projects = await _mediator.Send(request);

            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects);
        }

    }
}
