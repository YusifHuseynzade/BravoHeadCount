using BakuTargetDetails.Commands.Request;
using BakuTargetDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResidentalAreaDetails.Commands.Request;
using ResidentalAreaDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BakuTargetController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BakuTargetController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBakuTargetCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteBakuTargetCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllBakuTargetQueryRequest request)
        {
            var districts = await _mediator.Send(request);

            return Ok(districts);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBakuTargetCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdBakuTargetQueryRequest { Id = id };
            var residentalArea = await _mediator.Send(requestModel);

            return residentalArea != null
                ? (IActionResult)Ok(residentalArea)
                : NotFound(new { Message = "BakuTarget not found." });
        }

        [HttpGet("BakuTargetEmployees")]
        public async Task<IActionResult> GetBakuTargetEmployees([FromQuery] GetBakuTargetEmployeesQueryRequest request)
        {
            var employees = await _mediator.Send(request);

            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

    }
}
