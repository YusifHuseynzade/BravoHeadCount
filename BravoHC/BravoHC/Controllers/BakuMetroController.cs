using BakuMetroDetails.Commands.Request;
using BakuMetroDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResidentalAreaDetails.Commands.Request;
using ResidentalAreaDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BakuMetroController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BakuMetroController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBakuMetroCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteBakuMetroCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllBakuMetroQueryRequest request)
        {
            var districts = await _mediator.Send(request);

            return Ok(districts);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBakuMetroCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdBakuMetroQueryRequest { Id = id };
            var residentalArea = await _mediator.Send(requestModel);

            return residentalArea != null
                ? (IActionResult)Ok(residentalArea)
                : NotFound(new { Message = "BakuMetro not found." });
        }

        [HttpGet("BakuMetroEmployees")]
        public async Task<IActionResult> GetBakuMetroEmployees([FromQuery] GetBakuMetroEmployeesQueryRequest request)
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
