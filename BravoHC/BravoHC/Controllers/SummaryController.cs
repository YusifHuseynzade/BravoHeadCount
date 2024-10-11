
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduledDataDetails.Commands.Request;
using ScheduledDataDetails.Queries.Request;
using SectionDetails.Commands.Request;
using SectionDetails.Queries.Request;
using SickLeaveDetails.Queries.Request;
using SummaryDetails.Queries.Request;
using VacationScheduleDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class SummaryController : ControllerBase
	{
		private readonly IMediator _mediator;
		public SummaryController(IMediator mediator)
		{
			_mediator = mediator;
		}

        [HttpGet]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSummaryQueryRequest request)
        {
            var scheduledDatas = await _mediator.Send(request);

            return Ok(scheduledDatas);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdSummaryQueryRequest { Id = id };
            var position = await _mediator.Send(requestModel);

            return position != null
                ? (IActionResult)Ok(position)
                : NotFound(new { Message = "Summary not found." });
        }


    }
}
