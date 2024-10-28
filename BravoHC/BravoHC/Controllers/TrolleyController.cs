using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyOrderDetails.Commands.Request;
using MoneyOrderDetails.Queries.Request;
using PositionDetails.Commands.Request;
using PositionDetails.Queries.Request;
using TrolleyDetails.Commands.Request;
using TrolleyDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TrolleyController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Add([FromQuery] CreateTrolleyCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteTrolleyCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromQuery] UpdateTrolleyCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTrolleyQueryRequest request)
        {
            var Trolleys = await _mediator.Send(request);

            return Ok(Trolleys);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdTrolleyQueryRequest { Id = id };
            var Trolley = await _mediator.Send(requestModel);

            return Trolley != null
                ? (IActionResult)Ok(Trolley)
                : NotFound(new { Message = "Trolley not found." });
        }
        [HttpGet("trolleyhistory")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetHistory([FromQuery] GetTrolleyHistoryQueryRequest request)
        {
            var trolleyHistory = await _mediator.Send(request);

            if (trolleyHistory == null)
            {
                return NotFound();
            }

            return Ok(trolleyHistory);
        }
    }
}
