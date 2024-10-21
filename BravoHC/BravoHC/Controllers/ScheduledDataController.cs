
using Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PositionDetails.Queries.Request;
using ScheduledDataDetails.Commands.Request;
using ScheduledDataDetails.Queries.Request;
using ScheduledDataDetails.Queries.Response;
using ScheduledDataDetails.ScheduledDataExportService;
using SectionDetails.Commands.Request;
using SectionDetails.Queries.Request;
using SickLeaveDetails.Queries.Request;
using VacationScheduleDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ScheduledDataController : ControllerBase
	{
		private readonly IMediator _mediator;
        private readonly IApplicationDbContext _appDbContext;
        private readonly ScheduledDataExportService _exportService;
        public ScheduledDataController(IMediator mediator, IApplicationDbContext appDbContext, ScheduledDataExportService exportService)
		{
			_mediator = mediator;
            _appDbContext = appDbContext;
            _exportService = exportService;
		}

        [HttpPost("create-next-week")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
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
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
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

        [HttpGet]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllScheduledDataQueryRequest request)
        {
            var scheduledDatas = await _mediator.Send(request);

            return Ok(scheduledDatas);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdScheduledDataQueryRequest { Id = id };
            var position = await _mediator.Send(requestModel);

            return position != null
                ? (IActionResult)Ok(position)
                : NotFound(new { Message = "VacationSchedule not found." });
        }

        [HttpGet("export-scheduled-data")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> ExportScheduledDataToExcel(
           [FromQuery] int? projectId = null,
           [FromQuery] DateTime? weekDate = null)
        {
            // Validate: Both projectId and weekDate must be provided
            if (!projectId.HasValue || !weekDate.HasValue)
            {
                return BadRequest("Both Project ID and Week Date must be provided.");
            }

            var query = _appDbContext.ScheduledDatas.AsQueryable();

            // Filter by project ID
            query = query.Where(sd => sd.ProjectId == projectId.Value);

            // Calculate the start and end of the selected week
            var culture = new System.Globalization.CultureInfo("en-US");
            var firstDayOfWeek = DayOfWeek.Monday;

            var startOfWeek = weekDate.Value
                .Date.AddDays(-(7 + (int)weekDate.Value.DayOfWeek - (int)firstDayOfWeek) % 7)
                .ToUniversalTime();  // Ensure UTC

            var endOfWeek = startOfWeek.AddDays(7).AddSeconds(-1);  // Include the entire last day

            // Filter by week date range
            query = query.Where(sd => sd.Date >= startOfWeek && sd.Date <= endOfWeek);

            var scheduledData = await query
                .Include(sd => sd.Project)
                .Include(sd => sd.Employee)
                    .ThenInclude(e => e.Position)
                .Include(sd => sd.Employee)
                    .ThenInclude(e => e.Section)
                .Include(sd => sd.Plan)
                .ToListAsync();

            if (!scheduledData.Any())
            {
                return NotFound("No Scheduled Data found to export.");
            }

            // Export the data to Excel
            var fileContent = await _exportService.ExportScheduledDataToExcelAsync(scheduledData, projectId);

            return File(fileContent,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ScheduledData.xlsx");
        }

        [HttpGet("scheduledData-plans")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPlanQueryRequest request)
        {
            var plans = await _mediator.Send(request);

            return Ok(plans);
        }
    }
}
