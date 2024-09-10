using EmployeeDetails.Commands.Request;
using EmployeeDetails.Queries.Request;
using HeadCountDetails.Commands.Request;
using HeadCountDetails.ExcelImportService;
using HeadCountDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadCountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly HeadCountImportService _importService;
        public HeadCountController(IMediator mediator, HeadCountImportService importService)
        {
            _mediator = mediator;
            _importService = importService;
        }
     
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteHeadCountCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("bulk-delete")]
        public async Task<IActionResult> BulkDelete([FromBody] BulkDeleteHeadCountCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateHeadCountCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("bulk-update")]
        public async Task<IActionResult> BulkUpdate([FromBody] BulkUpdateHeadCountCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllHeadCountQueryRequest request)
        {
            var headCount = await _mediator.Send(request);

            return Ok(headCount);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdHeadCountQueryRequest { Id = id };
            var headCount = await _mediator.Send(requestModel);

            return headCount != null
                ? (IActionResult)Ok(headCount)
                : NotFound(new { Message = "HeadCount not found." });
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

        [HttpPut("update-hcnumber-order")]
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
    }
}
