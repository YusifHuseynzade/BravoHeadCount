using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PositionDetails.Commands.Request;
using PositionDetails.Queries.Request;
using SettingFinanceOperationDetails.Commands.Request;
using SettingFinanceOperationDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingFinanceOperationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SettingFinanceOperationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Add([FromBody] CreateSettingFinanceOperationCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteSettingFinanceOperationCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromBody] UpdateSettingFinanceOperationCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSettingFinanceOperationQueryRequest request)
        {
            var SettingFinanceOperations = await _mediator.Send(request);

            return Ok(SettingFinanceOperations);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdSettingFinanceOperationQueryRequest { Id = id };
            var SettingFinanceOperation = await _mediator.Send(requestModel);

            return SettingFinanceOperation != null
                ? (IActionResult)Ok(SettingFinanceOperation)
                : NotFound(new { Message = "SettingFinanceOperation not found." });
        }
    }
}
