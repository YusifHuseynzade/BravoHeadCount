
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SectionDetails.Commands.Request;
using SectionDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class SectionController : ControllerBase
	{
		private readonly IMediator _mediator;
		public SectionController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateSectionCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteSectionCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateSectionCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] GetAllSectionQueryRequest request)
		{
			var sections = await _mediator.Send(request);

			return Ok(sections);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var requestModel = new GetByIdSectionQueryRequest { Id = id };
			var section = await _mediator.Send(requestModel);

			return section != null
				? (IActionResult)Ok(section)
				: NotFound(new { Message = "Section not found." });
		}


        [HttpGet("GetSectionSubSections")]
        public async Task<IActionResult> GetSectionSubSection([FromQuery] GetSectionSubSectionQueryRequest request)
        {
            var sectionSubSection = await _mediator.Send(request);

            if (sectionSubSection == null)
            {
                return NotFound();
            }

            return Ok(sectionSubSection);
        }

    }
}
