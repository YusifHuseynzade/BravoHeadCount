using EmployeeDetails.Commands.Request;
using EmployeeDetails.Queries.Request;
using HeadCountDetails.Commands.Request;
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
        public HeadCountController(IMediator mediator)
        {
            _mediator = mediator;
        }
     
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteHeadCountCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateHeadCountCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
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
    }
}
