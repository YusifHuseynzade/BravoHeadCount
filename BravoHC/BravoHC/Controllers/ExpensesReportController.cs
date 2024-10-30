using Domain.Entities;
using EndOfMonthReportDetails.Queries.Request;
using ExpensesReportDetails.Commands.Request;
using ExpensesReportDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PositionDetails.Commands.Request;
using PositionDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ExpensesReportController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Add([FromQuery] CreateExpensesReportCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteExpensesReportCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromQuery] UpdateExpensesReportCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllExpensesReportQueryRequest request)
        {
            var ExpensesReports = await _mediator.Send(request);

            return Ok(ExpensesReports);
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdExpensesReportQueryRequest { Id = id };
            var ExpensesReport = await _mediator.Send(requestModel);

            return ExpensesReport != null
                ? (IActionResult)Ok(ExpensesReport)
                : NotFound(new { Message = "ExpensesReport not found." });
        }
        [HttpGet("expensesreporthistory")]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetHistory([FromQuery] GetExpensesReportHistoryQueryRequest request)
        {
            var expensesReportHistory = await _mediator.Send(request);

            if (expensesReportHistory == null)
            {
                return NotFound();
            }

            return Ok(expensesReportHistory);
        }
    }
}
