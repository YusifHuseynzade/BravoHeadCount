using Domain.Entities;
using EncashmentDetails.Commands.Request;
using EncashmentDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PositionDetails.Commands.Request;
using PositionDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncashmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EncashmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Add([FromBody] CreateEncashmentCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteEncashmentCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromBody] UpdateEncashmentCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllEncashmentQueryRequest request)
        {
            var Encashments = await _mediator.Send(request);

            return Ok(Encashments);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdEncashmentQueryRequest { Id = id };
            var Encashment = await _mediator.Send(requestModel);

            return Encashment != null
                ? (IActionResult)Ok(Encashment)
                : NotFound(new { Message = "Encashment not found." });
        }
        [HttpPut("UpdateJobDescription")]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromForm] UpdateEncashmentFileCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
