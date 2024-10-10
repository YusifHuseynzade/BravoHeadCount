
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduledDataDetails.Commands.Request;
using SectionDetails.Commands.Request;
using SectionDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ScheduledDataController : ControllerBase
	{
		private readonly IMediator _mediator;
		public ScheduledDataController(IMediator mediator)
		{
			_mediator = mediator;
		}

        [HttpPost("create-next-week")]
        public async Task<IActionResult> CreateNextWeekScheduledData()
        {
            var result = await _mediator.Send(new CreateScheduledDataCommandRequest());

            if (result.IsSuccess)
            {
                return Ok("Scheduled data for the next week has been successfully created.");
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPut("update-weekly")]
        public async Task<IActionResult> UpdateWeeklyScheduledData([FromBody] UpdateScheduledDataCommandRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            var result = await _mediator.Send(request);

            if (result.IsSuccess)
            {
                return Ok("Scheduled data for the week has been successfully updated.");
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

    }
}
