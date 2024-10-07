using EmployeeDetails.Commands.Request;
using EmployeeDetails.ExcelImportService;
using EmployeeDetails.Queries.Request;
using HeadCountDetails.ExcelImportService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly EmployeeImportService _importService;
        public EmployeeController(IMediator mediator, EmployeeImportService importService)
        {
            _mediator = mediator;
            _importService = importService;
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Add([FromBody] CreateEmployeeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteEmployeeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllEmployeeQueryRequest request)
        {
            var employees = await _mediator.Send(request);

            return Ok(employees);
        }

        [HttpGet("all-employee-ids")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAllEmployeeIds()
        {
            var employeeIds = await _mediator.Send(new GetAllEmployeeIdsQueryRequest());
            return Ok(employeeIds);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdEmployeeQueryRequest { Id = id };
            var employee = await _mediator.Send(requestModel);

            return employee != null
                ? (IActionResult)Ok(employee)
                : NotFound(new { Message = "Employee not found." });
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

        [HttpPut("update-headcount")]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromBody] BulkUpdateEmployeeHeadCountCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("update-parentid")]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> UpdateParentId([FromBody] AddParentIdCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        // Yeni endpoint: RecruiterComment güncelleme
        [HttpPut("update-recruiter-comment")]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> UpdateRecruiterComment([FromBody] UpdateRecruiterCommentCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        // Yeni endpoint: EmployeeLocation güncelleme
        [HttpPut("update-employee-location")]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> UpdateEmployeeLocation([FromBody] UpdateEmployeeLocationCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("update-employee-image")]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> UpdateEmployeeImage([FromForm] UpdateEmployeeImageCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Message);
        }

    }
}
