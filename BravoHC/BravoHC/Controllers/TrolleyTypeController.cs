using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyOrderDetails.Commands.Request;
using MoneyOrderDetails.Queries.Request;
using PositionDetails.Commands.Request;
using PositionDetails.Queries.Request;
using TrolleyTypeDetails.Commands.Request;
using TrolleyTypeDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TrolleyTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Add([FromBody] CreateTrolleyTypeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteTrolleyTypeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromBody] UpdateTrolleyTypeCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTrolleyTypeQueryRequest request)
        {
            var TrolleyTypes = await _mediator.Send(request);

            return Ok(TrolleyTypes);
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdTrolleyTypeQueryRequest { Id = id };
            var TrolleyType = await _mediator.Send(requestModel);

            return TrolleyType != null
                ? (IActionResult)Ok(TrolleyType)
                : NotFound(new { Message = "TrolleyType not found." });
        }
    }
}
