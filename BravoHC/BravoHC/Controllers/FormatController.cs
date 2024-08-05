using FormatDetails.Commands.Request;
using FormatDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormatController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FormatController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateFormatCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteFormatCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFormatCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllFormatQueryRequest request)
        {
            var formats = await _mediator.Send(request);

            return Ok(formats);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdFormatQueryRequest { Id = id };
            var format = await _mediator.Send(requestModel);

            return format != null
                ? (IActionResult)Ok(format)
                : NotFound(new { Message = "Format not found." });
        }
    }
}
