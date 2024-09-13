using EmployeeDetails.Commands.Request;
using EmployeeDetails.ExcelImportService;
using EmployeeDetails.Queries.Request;
using HeadCountDetails.ExcelImportService;
using MediatR;
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
        public async Task<IActionResult> Add([FromBody] CreateEmployeeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteEmployeeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllEmployeeQueryRequest request)
        {
            var employees = await _mediator.Send(request);

            return Ok(employees);
        }

        [HttpGet("all-employee-ids")]
        public async Task<IActionResult> GetAllEmployeeIds()
        {
            var employeeIds = await _mediator.Send(new GetAllEmployeeIdsQueryRequest());
            return Ok(employeeIds);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdEmployeeQueryRequest { Id = id };
            var employee = await _mediator.Send(requestModel);

            return employee != null
                ? (IActionResult)Ok(employee)
                : NotFound(new { Message = "Employee not found." });
        }

        [HttpPost("importexceldata")]
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
        public async Task<IActionResult> Update([FromBody] BulkUpdateEmployeeHeadCountCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("update-parentid")]
        public async Task<IActionResult> UpdateParentId([FromBody] AddParentIdCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

    }
}
