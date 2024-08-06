using EmployeeDetails.Commands.Request;
using EmployeeDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdEmployeeQueryRequest { Id = id };
            var employee = await _mediator.Send(requestModel);

            return employee != null
                ? (IActionResult)Ok(employee)
                : NotFound(new { Message = "Employee not found." });
        }
    }
}
