using EndOfMonthReportDetails.Commands.Request;
using EndOfMonthReportDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PositionDetails.Queries.Request;

namespace FinanceOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndOfMonthReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EndOfMonthReportController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Add([FromQuery] CreateEndOfMonthReportCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteEndOfMonthReportCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromQuery] UpdateEndOfMonthReportCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPositionQueryRequest request)
        {
            var EndOfMonthReports = await _mediator.Send(request);

            return Ok(EndOfMonthReports);
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdEndOfMonthReportQueryRequest { Id = id };
            var EndOfMonthReport = await _mediator.Send(requestModel);

            return EndOfMonthReport != null
                ? (IActionResult)Ok(EndOfMonthReport)
                : NotFound(new { Message = "EndOfMonthReport not found." });
        }
        [HttpGet("endofmonthreporthistory")]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetHistory([FromQuery] GetEndOfMonthReportHistoryQueryRequest request)
        {
            var endOfMonthReportHistory = await _mediator.Send(request);

            if (endOfMonthReportHistory == null)
            {
                return NotFound();
            }

            return Ok(endOfMonthReportHistory);
        }
    }
}
