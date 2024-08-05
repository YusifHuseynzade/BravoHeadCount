using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubSectionDetails.Commands.Request;
using SubSectionDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class SubSectionController : ControllerBase
	{
		private readonly IMediator _mediator;
		public SubSectionController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateSubSectionCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteSubSectionCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateSubSectionCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] GetAllSubSectionQueryRequest request)
		{
			var sections = await _mediator.Send(request);

			return Ok(sections);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var requestModel = new GetByIdSubSectionQueryRequest { Id = id };
			var subSection = await _mediator.Send(requestModel);

			return subSection != null
				? (IActionResult)Ok(subSection)
				: NotFound(new { Message = "SubSection not found." });
		}

    }
}
