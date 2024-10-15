using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PositionDetails.Commands.Request;
using PositionDetails.Queries.Request;
using SickLeaveDetails.Commands.Request;
using SickLeaveDetails.Queries.Request;
using VacationScheduleDetails.Commands.Request;
using VacationScheduleDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VacationScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Add([FromQuery] CreateVacationScheduleCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteVacationScheduleCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromQuery] UpdateVacationScheduleCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllVacationScheduleQueryRequest request)
        {
            var positions = await _mediator.Send(request);

            return Ok(positions);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdVacationScheduleQueryRequest { Id = id };
            var position = await _mediator.Send(requestModel);

            return position != null
                ? (IActionResult)Ok(position)
                : NotFound(new { Message = "VacationSchedule not found." });
        }
    }
}
