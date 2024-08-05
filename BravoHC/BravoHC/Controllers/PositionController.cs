
using FormatDetails.Commands.Request;
using FormatDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PositionDetails.Commands.Request;
using PositionDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PositionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePositionCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeletePositionCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePositionCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPositionQueryRequest request)
        {
            var positions = await _mediator.Send(request);

            return Ok(positions);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdPositionQueryRequest { Id = id };
            var position = await _mediator.Send(requestModel);

            return position != null
                ? (IActionResult)Ok(position)
                : NotFound(new { Message = "Position not found." });
        }
    }
}
