using Common.Interfaces;
using EmployeeDetails.Commands.Request;
using EmployeeDetails.Queries.Request;
using HeadCountDetails.Commands.Request;
using HeadCountDetails.ExcelImportService;
using HeadCountDetails.HeadCountExportedService;
using HeadCountDetails.Queries.Request;
using Infrastructure;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadCountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly HeadCountImportService _importService;
        private readonly HeadCountExportService _headCountExportService;
        private readonly IApplicationDbContext _appDbContext;
        public HeadCountController(IMediator mediator, HeadCountImportService importService, HeadCountExportService headCountExportService, IApplicationDbContext appDbContext)
        {
            _mediator = mediator;
            _importService = importService;
            _headCountExportService = headCountExportService;
            _appDbContext = appDbContext;


        }
     
        [HttpDelete]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteHeadCountCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("bulk-delete")]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> BulkDelete([FromBody] BulkDeleteHeadCountCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromBody] UpdateHeadCountCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("bulk-update")]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> BulkUpdate([FromBody] BulkUpdateHeadCountCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllHeadCountQueryRequest request)
        {
            var headCount = await _mediator.Send(request);

            return Ok(headCount);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdHeadCountQueryRequest { Id = id };
            var headCount = await _mediator.Send(requestModel);

            return headCount != null
                ? (IActionResult)Ok(headCount)
                : NotFound(new { Message = "HeadCount not found." });
        }
        [HttpPost("importexceldata")]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Import([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a valid Excel file.");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            await _importService.ImportAsync(stream);

            return Ok("File imported successfully.");
        }

        [HttpPut("update-hcnumber-order")]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> UpdateHeadCountOrder([FromBody] BulkUpdateHCNumberCommandRequest request)
        {
            // MediatR ile handler'a yönlendiriyoruz
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("export-headcounts")]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> ExportHeadCountsToExcel([FromQuery] int? projectId = null)
        {
            // Tüm HeadCount kayıtlarını çekiyoruz (filtreleme serviste yapılacak).
            var headCounts = await _appDbContext.HeadCounts
                .Include(hc => hc.Project)
                .Include(hc => hc.Section)
                .Include(hc => hc.SubSection)
                .Include(hc => hc.Position)
                .Include(hc => hc.Employee)
                .ToListAsync();

            if (!headCounts.Any())
            {
                return NotFound("No HeadCount found to export.");
            }

            // Filtreleme işlemi servis tarafında yapılacak
            var fileContent = await _headCountExportService.ExportHeadCountsToExcelAsync(headCounts, projectId);

            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HeadCounts.xlsx");
        }
    }
}
